using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Data
{
    // public static int age = -1; //年齢
    // public static string sex = "None"; //性別 F or M
    // public static string dominantHand = "None"; // 利き手 R or L
    // public static string vision = "None"; //視力 normal or corrected-to-normal
    public static string ID = "None"; //fileNameになる
    public static string Date = "None";
    public static List<string> order = new List<string>() {"FlankerTaskScene", "FarFlankerTask", "BarrierFlankerTask", "2DFlankerTask"};
    public static List<string> order_tmp;
    public static int taskCount = 0;
    public static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
}
