using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;


public class TaskController : MonoBehaviour
{
    //アタッチの必要があるやつ
    public GameObject leftCongruent;
    public GameObject leftIncongruent; 
    public GameObject rightCongruent; 
    public GameObject rightIncongruent; 
    public GameObject cameraRigObject;
    public string fileName;
    

    //アタッチの必要なし
    private GameObject currentInstance; 
    private int STIMULI_NUM = 40; //刺激の数，ホントは40
    private string[] stimuliNames = {"leftCongruent", "leftIncongruent", "rightCongruent", "rightIncongruent"};
    private float WAIT_TIME = 5f; //次の刺激までの待機時間
    private string selected = "init";
    private float cameraHeight;
    OVRCameraRig cameraRig;
    AudioSource audioSource;
    string dateTime = "Sample";
    string dirPath;
    string filePath;
    private StreamWriter streamWriter;

    
    // Start is called before the first frame update
    void Start()
    {
        // カメラの高さ
        cameraRig = cameraRigObject.GetComponent<OVRCameraRig>();
        cameraHeight = cameraRig.centerEyeAnchor.position.y;

        // 各刺激名を10個ずつリストに追加
        var stimuliList = new List<string>();
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 10; j++){
                stimuliList.Add(stimuliNames[i]);
            }
        }
        // ランダムにシャッフル
        stimuliList = stimuliList.OrderBy(a => Guid.NewGuid()).ToList();

        // 音
        audioSource = GetComponent<AudioSource>();

        // 保存の準備
        Debug.Log(Application.persistentDataPath);
        dirPath = Application.persistentDataPath + "/" + dateTime;

        // ディレクトリの存在確認，デバッグ用かな
        if(!Directory.Exists(dirPath)){
            Directory.CreateDirectory(dirPath); //なかったらつくる
        }
        
        filePath = dirPath + "/" + fileName + ".csv";
        // ファイルの存在確認
        if(!File.Exists(filePath)){
            File.Create(filePath); //なかったら作る
        }
        // ラベルを追加
        AddData(filePath, "stimulus, response, TF, RT"); 

        IEnumerator runTask = RunTask(stimuliList);
        StartCoroutine(runTask);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cameraRig.centerEyeAnchor.position.y :");
        Debug.Log(cameraRig.centerEyeAnchor.position.y);
        Debug.Log("Application.persistentDataPath");
        Debug.Log(Application.persistentDataPath);

        // if (OVRInput.GetDown(OVRInput.Button.One)){
        //     audioSource.Play();
        // }
            
    }

    private IEnumerator RunTask(List<string> stimuliList){
        Debug.Log("Now RunTask...");
        // Debug.Log(string.Join(",", stimuliList));
        
        for(int i = 0; i < STIMULI_NUM; i++){
            Debug.Log(i);
            Debug.Log(stimuliList[i]); //なぜか正しく動作しないときがある．原因不明

            // csv書き込み用の変数
            string stimulus = stimuliList[i];
            string response = "";
            string TF = "";
            string RT = "";

            // 一応ね
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

            //名前と同じ刺激をインスタンス化
            // if (stimulus == "leftCongruent"){
            //     currentInstance = Instantiate(leftCongruent, new Vector3(leftCongruent.transform.position.x, cameraHeight, leftCongruent.transform.position.z), Quaternion.identity);
            //     stopWatch.Start();
            // } else if (stimulus == "leftIncongruent"){
            //     currentInstance = Instantiate(leftIncongruent, new Vector3(leftIncongruent.transform.position.x, cameraHeight, leftIncongruent.transform.position.z), Quaternion.identity);
            //     stopWatch.Start();
            // } else if (stimulus == "rightCongruent"){
            //     currentInstance = Instantiate(rightCongruent, new Vector3(rightCongruent.transform.position.x, cameraHeight, rightCongruent.transform.position.z), Quaternion.identity);
            //     stopWatch.Start();
            // } else if (stimulus == "rightIncongruent"){
            //     currentInstance = Instantiate(rightIncongruent, new Vector3(rightIncongruent.transform.position.x, cameraHeight, rightIncongruent.transform.position.z), Quaternion.identity);
            //     stopWatch.Start();
            // } else{
            //     Debug.Log("ERROR : stimuli instantiate");
            //     yield break;
            // }

            if (stimulus == "leftCongruent"){
                currentInstance = Instantiate(leftCongruent);
                stopWatch.Start();
            } else if (stimulus == "leftIncongruent"){
                currentInstance = Instantiate(leftIncongruent);
                stopWatch.Start();
            } else if (stimulus == "rightCongruent"){
                currentInstance = Instantiate(rightCongruent);
                stopWatch.Start();
            } else if (stimulus == "rightIncongruent"){
                currentInstance = Instantiate(rightIncongruent);
                stopWatch.Start();
            } else{
                Debug.Log("ERROR : stimuli instantiate");
                yield break;
            }
            

            // ボタンが押されるまで待つ,
            yield return new WaitUntil(() => {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    stopWatch.Stop();
                    selected = "right";
                    response = "right";
                    return true;
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    stopWatch.Stop();
                    selected = "left";
                    response = "left";
                    return true;
                }
                return false;
            });


            // 正誤判定とそのときの処理
            if (stimulus.Contains(selected)){
                // TestText.text = "CORRECT";
                Debug.Log("CORRECT!!!");
                TF = "1";
            }else{
                // エラー音
                audioSource.Play();
                // TestText.text = "MISS";
                Debug.Log("MISS!!!");
                TF = "0";
            }

            // インスタンス削除
            Destroy(currentInstance);

            // 反応時間
            double RT_d = stopWatch.Elapsed.TotalMilliseconds;
            RT = RT_d.ToString();

            // 選択を初期化
            selected = "init";

            // csvに書き込み
            string data = stimulus + "," + response + "," + TF + "," + RT;
            AddData(filePath, data);

            // 次の刺激表示まで待機
            yield return new WaitForSeconds(WAIT_TIME);

            
        }
    }

    static void AddData(string filePath, string data){
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8)){
            sw.WriteLine(data);
        }
    }
}
