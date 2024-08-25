using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveData : MonoBehaviour
{
    string totalTime;
    string dirPath;
    string filePath;
    // Start is called before the first frame update
    void Start()
    {
        Data.sw.Stop();
        double totalTime_d = Data.sw.Elapsed.TotalMilliseconds;
        totalTime = totalTime_d.ToString();

        dirPath = Application.persistentDataPath + "/" + Data.ID;
        filePath = dirPath + "/" + Data.ID + "Data" + ".csv";

        string order = string.Join(",", Data.order);

        //　ラベル
        AddData(filePath, "ID, Date, TotalTime, order"); 
        string data = Data.ID + "," + Data.Date + "," + totalTime + "," + order;
        AddData(filePath, data); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static void AddData(string filePath, string data){
        using (StreamWriter streamWriter = new StreamWriter(filePath, true, Encoding.UTF8)){
            streamWriter.WriteLine(data);
        }
    }
}
