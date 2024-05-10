using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //ridigbodyコンポーネントにアクセスできるようにする
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // キーボードから水平・鉛直の入力を受け付ける
        float moveHorizontal = Input.GetAxis("Horizontal");
        float mobeVertical = Input.GetAxis("Vertical");

        //newでインスタンス化　Vector3型のmovement変数にいれてる
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, mobeVertical);

        //このボールに力を加える
        rb.AddForce(movement * speed * Time.deltaTime);


    }
}
