using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using System.Linq;
using System;
using System.Threading.Tasks;


public class TaskController : MonoBehaviour
{
    public GameObject leftCongruent; //0
    public GameObject leftIncongruent; //1
    public GameObject rightCongruent; //2
    public GameObject rightIncongruent; //3
    private GameObject currentInstance; 
    private int STIMULI_NUM = 10; //刺激の数，ホントは40

    
    // Start is called before the first frame update
    void Start()
    {
        // 各タスクを10個ずつリストに追加
        string[] stimuliNames = {"leftCongruent", "leftIncongruent", "rightCongruent", "rightIncongruent"};
        // Debug.Log(taskNames[0]); 
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
       
    }

    private IEnumerator RunTask(List<string> stimuliList){

        for(int i = 0; i < STIMULI_NUM; i++){
            Debug.Log(i);
            Debug.Log(stimuliList[i]);
            yield return new WaitForSeconds(1f);
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
