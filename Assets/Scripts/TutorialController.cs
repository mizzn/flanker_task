using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;

public class TutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //まずは個人のデータを初期化 値は適当
        Data.age = 20;
        Data.sex = "F";
        Data.dominantHand = "L";
        Data.vision = "normal";
        Data.ID = DateTime.Now.ToString("yyyyMMddHHmmss");

        // orderをシャッフル
        Data.order = Data.order.OrderBy(a => Guid.NewGuid()).ToList();
        Data.order_tmp = new List<string>(Data.order);

        // データをいれるフォルダを作っておく
        string dirPath = Application.persistentDataPath + "/" + Data.ID;
        Directory.CreateDirectory(dirPath);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("next", 3f); //3秒後遷移
    }

    void next(){
        SceneManager.LoadScene(Data.order_tmp[0]);
    }
}
