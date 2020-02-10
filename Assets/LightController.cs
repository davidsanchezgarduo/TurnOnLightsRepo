using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float lightRange;
    private Transform lightArea;
    // Start is called before the first frame update
    void Start()
    {
        lightArea = transform.GetChild(0);
        lightArea.localScale = new Vector3(lightRange*2,0.01f, lightRange*2);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
