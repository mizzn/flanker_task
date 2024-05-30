using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using System.Linq;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;


public class TaskController : MonoBehaviour
{
    public GameObject leftCongruent; //0
    public GameObject leftIncongruent; //1
    public GameObject rightCongruent; //2
    public GameObject rightIncongruent; //3
    private GameObject currentInstance; 
    private int STIMULI_NUM = 10; //刺激の数，ホントは40
    private string[] stimuliNames = {"leftCongruent", "leftIncongruent", "rightCongruent", "rightIncongruent"};
    private float WAIT_TIME = 5f; //次の刺激までの待機時間
    private string selected = "init";

    
    // Start is called before the first frame update
    void Start()
    {

        // 各刺激名を10個ずつリストに追加
        var stimuliList = new List<string>();
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 10; j++){
                stimuliList.Add(stimuliNames[i]);
            }
        }
        // ランダムにシャッフル
        stimuliList = stimuliList.OrderBy(a => Guid.NewGuid()).ToList();
        Debug.Log(string.Join(",", stimuliList));


        //InvokeRepeating("Toggle", 0f, 3f);
        // Toggle();

        IEnumerator runTask = RunTask(stimuliList);
        StartCoroutine(runTask);

    }

    // Update is called once per frame
    void Update()
    {
        // トリガーを常に監視
        // WatchTrigger();
    }

/*
    void WatchTrigger(){
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)){
            rightPressed = true;
        } else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
            leftPressed = true;
        }
    }

    void InitTrigger(string selected){
        if (selected == "right"){
            rightPressed = false;
        }else if (selected == "left"){
            leftPressed = false;
        }else if (selected == "init"){
            // 何もしない
        }else{
            Debug.Log("ERROR : InitTrigger");
        }
    }
*/

    private IEnumerator RunTask(List<string> stimuliList){
        Debug.Log("Now RunTask...");
        Debug.Log(string.Join(",", stimuliList));
        /* 
            フランカー課題を実行する
            Args:
            List<string> stimuliList : 刺激名が試行回数分，シャッフルした状態で入れられたリスト
        */
        for(int i = 0; i < stimuliList.Count; i++){
            // Debug.Log(i);
            // Debug.Log(stimuliList[i]); //なぜか正しく動作しないときがある．原因不明
            string stimulus = stimuliList[i];

            // 一応ね
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            //名前と同じ刺激をインスタンス化  "leftCongruent", "leftIncongruent", "rightCongruent", "rightIncongruent"
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

            // ボタンが押されるまで待つ
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
                // TestText.text = "MISS";
                Debug.Log("MISS!!!");
            }
            
            // インスタンス削除
            Destroy(currentInstance);
            // 選択を初期化
            selected = "init";

            // 次の刺激表示まで待機
            yield return new WaitForSeconds(WAIT_TIME);

            
        }

        /*
        while (cnt<STIMULI_NUM)
        {
            // ゲームオブジェクトを生成
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            currentInstance = Instantiate(rightIncongruent);
            // startTime = Time.time;

            // ボタンの押下を待つ
            yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger));
            Destroy(currentInstance);


            // 次の生成まで少し待つ（例: 1秒）
            yield return new WaitForSeconds(1f);
            cnt++;
        }*/
    }


   
}
