using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public Material[] wallMaterial; //初期値はUnity側で設定できるみたい
    Renderer rend;

    public Text displayText;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        displayText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col){
        if (col.gameObject.name == "player-ball"){
            displayText.text = "Ouch!";
            rend.sharedMaterial = wallMaterial[0];
        }
    }

    private void OnCollisionExit(Collision col){
        if (col.gameObject.name == "player-ball"){
            displayText.text = "Keep Rolling...";
            rend.sharedMaterial = wallMaterial[1];
        }
    }


}
