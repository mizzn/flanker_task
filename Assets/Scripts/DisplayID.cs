using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayID : MonoBehaviour
{
    public TextMeshPro id;

    // Start is called before the first frame update
    void Start()
    {
        id.text = Data.ID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
