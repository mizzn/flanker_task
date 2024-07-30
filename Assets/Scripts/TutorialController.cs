using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;

public class TutorialController : MonoBehaviour
{
    //アタッチの必要があるやつ
    public GameObject leftCongruent;
    public GameObject leftIncongruent; 
    public GameObject rightCongruent; 
    public GameObject rightIncongruent;
    public GameObject twodLeftCongruent;
    public GameObject FarRightIncongruent; 
    public GameObject barrier;
    AudioSource audioSource;

    private float WAIT_TIME = 5f; //次の刺激までの待機時間
    private GameObject currentInstance; 
    private GameObject barrierInstance;
    private string selected = "init";

    // Start is called before the first frame update
    void Start()
    {
        //まずは個人のデータを初期化 値は適当
        // Data.age = 20;
        // Data.sex = "F";
        // Data.dominantHand = "L";
        // Data.vision = "normal";
        Data.ID = MakeID();
        Data.Date = DateTime.Now.ToString("yyyyMMddHHmmss");
        Debug.Log(Data.ID);
        Debug.Log(Data.Date);

        audioSource = GetComponent<AudioSource>();

        // orderをシャッフル
        Data.order = Data.order.OrderBy(a => Guid.NewGuid()).ToList();
        Data.order_tmp = new List<string>(Data.order);

        // データをいれるフォルダを作っておく
        string dirPath = Application.persistentDataPath + "/" + Data.ID;
        Directory.CreateDirectory(dirPath);

        List<string> stimuliList_A = new List<string>() {"leftCongruent", "rightIncongruent", "leftIncongruent", "rightCongruent"};
        List<string> stimuliList_B = new List<string>() {"twodleftCongruent", "FarrightIncongruent", "leftIncongruent", "rightCongruent"};

        IEnumerator runTutorial = RunTutorial(stimuliList_A, stimuliList_B);
        StartCoroutine(runTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        // Invoke("next", 3f); //3秒後遷移
    }

    private IEnumerator RunTutorial(List<string> stimuliList_A, List<string> stimuliList_B){
        Debug.Log("Now RunTutorial...");
        
        for(int i = 0; i < 4; i++){
            string stimulus = stimuliList_A[i];
            
            // 一応ね
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            if (stimulus == "leftCongruent"){
                currentInstance = Instantiate(leftCongruent);
            } else if (stimulus == "leftIncongruent"){
                currentInstance = Instantiate(leftIncongruent);
            } else if (stimulus == "rightCongruent"){
                currentInstance = Instantiate(rightCongruent);
            } else if (stimulus == "rightIncongruent"){
                currentInstance = Instantiate(rightIncongruent);
            } else{
                Debug.Log("ERROR : stimuli instantiate");
                yield break;
            }
            

            // ボタンが押されるまで待つ,
            yield return new WaitUntil(() => {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    selected = "right";
                    return true;
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    selected = "left";
                    return true;
                }
                return false;
            });


            // 正誤判定とそのときの処理
            if (stimulus.Contains(selected)){
                Debug.Log("CORRECT!!!");
            }else{
                // エラー音
                audioSource.Play();
                Debug.Log("MISS!!!");
            }

            // インスタンス削除
            Destroy(currentInstance);

            // 選択を初期化
            selected = "init";

            // 次の刺激表示まで待機
            yield return new WaitForSeconds(WAIT_TIME);
        }
        

        // 遠いときとか
        for(int i = 0; i < 4; i++){
            string stimulus = stimuliList_B[i];

            if (i == 2){
                barrierInstance = Instantiate(barrier);
            }

            // 一応ね
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            if (stimulus == "twodleftCongruent"){
                currentInstance = Instantiate(twodLeftCongruent);
            } else if (stimulus == "leftIncongruent"){
                currentInstance = Instantiate(leftIncongruent);
            } else if (stimulus == "rightCongruent"){
                currentInstance = Instantiate(rightCongruent);
            } else if (stimulus == "FarrightIncongruent"){
                currentInstance = Instantiate(FarRightIncongruent);
            } else{
                Debug.Log("ERROR : stimuli instantiate");
                yield break;
            }
            

            // ボタンが押されるまで待つ,
            yield return new WaitUntil(() => {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    selected = "right";
                    return true;
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    selected = "left";
                    return true;
                }
                return false;
            });


            // 正誤判定とそのときの処理
            if (stimulus.Contains(selected)){
                // TestText.text = "CORRECT";
                Debug.Log("CORRECT!!!");
            }else{
                // エラー音
                audioSource.Play();
                // TestText.text = "MISS";
                Debug.Log("MISS!!!");
            }

            // インスタンス削除
            Destroy(currentInstance);
            if (i == 2){
                Destroy(barrierInstance);
            }

            // 選択を初期化
            selected = "init";

            // 次の刺激表示まで待機
            yield return new WaitForSeconds(WAIT_TIME);
        }
    
        next();
    }

    string MakeID(){
        List<char> id = new List<char>();
        System.Random random = new System.Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for(int i = 0; i < 5; i++){
            int num = random.Next(chars.Length);
            id.Add(chars[num]);
        }
        return string.Join("", id);
    }

    void next(){
        SceneManager.LoadScene(Data.order_tmp[0]);
        // SceneManager.LoadScene("EndScene"); // Debug
    }
}
