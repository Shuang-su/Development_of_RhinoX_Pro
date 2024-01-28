using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printtest : MonoBehaviour
{
    public string myName;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("My name is " + myName);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Print Update");
    }
}
