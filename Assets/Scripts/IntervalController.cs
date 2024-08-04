using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntervalController : MonoBehaviour
{
    public GameObject blind;
    private float WAIT_TIME = 1f;

    // Start is called before the first frame update
    void Start()
    {
        blind.SetActive(false);

        IEnumerator run = Run();
        StartCoroutine(run);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Run(){
        // 右が押されるまで待つ
        yield return new WaitUntil(() => {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)){return true;}
            return false;
        });

        // 左が押されるまで待つ
        yield return new WaitUntil(() => {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){return true;}
            return false;
        });

        // 右が押されるまで待つ
        yield return new WaitUntil(() => {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)){return true;}
            return false;
        });

        blind.SetActive(true);
        Debug.Log("Finish interval");

        yield return new WaitForSeconds(WAIT_TIME);
        SceneManager.LoadScene(Data.order_tmp[0]);
    }

    private void PrintData(){
        // Debug.Log(Data.age);
        // Debug.Log(Data.sex);
        // Debug.Log(Data.dominantHand);
        // Debug.Log(Data.vision);
        Debug.Log(Data.ID);
        Debug.Log(string.Join(",", Data.order));
        Debug.Log(string.Join(",", Data.order_tmp));
        
    }
}
