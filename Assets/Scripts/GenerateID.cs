using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using System.IO;
using System.Text;

public class GenerateID : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> nums = new List<string>();
        for(int i = 100; i < 1000; i++){
            nums.Add(i.ToString());
        }
        // Debug.Log(string.Join(",", nums));

        // string path = "Assets/IDs.csv";
        string path = Application.persistentDataPath + "IDs.csv";
        Debug.Log(path);
        AddData(path, "ID, USED"); 

        System.Random random = new System.Random();
        for(int i = 0; i < 50; i++){
            int r = random.Next(nums.Count);
            string data = nums[r] + "," + "0";
            AddData(path, data);
            nums.Remove(nums[r]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void AddData(string filePath, string data){
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8)){
            sw.WriteLine(data);
        }
    }
}
