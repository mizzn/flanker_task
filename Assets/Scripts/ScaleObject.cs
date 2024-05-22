using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    private Vector3 initialScale; //初期サイズを受け取るための変数
    // private GameObject gameObject; //測りたいオブジェクトを受け取るための変数

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        initialScale = bounds.size;

        Debug.Log("Initial Scale x: " + initialScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
