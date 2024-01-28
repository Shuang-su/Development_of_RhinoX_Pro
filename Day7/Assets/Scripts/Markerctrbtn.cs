using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ximmerse.XR.Tag;
using Ximmerse.XR;

public class Markerctrbtn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject testBtn;
    private MeshRenderer testbtn;
    private string SN;
    public void GetMarkerctrbtn()

    {
        SN = XimSystemApi.XimGetSN();
        testbtn = testBtn.GetComponent<MeshRenderer>();
        
        MarkerController markerbtn = new MarkerController();

        bool triggerstate = markerbtn.TriggerButtonDown;
        bool appstate = markerbtn.AppButtonDown;

        Debug.Log("triggerstate is" + triggerstate);

        if(triggerstate)
        {
            Debug.Log("Trigger button down");
            testbtn.material.color = Color.blue;
        }
        if(appstate)
        {
            Debug.Log("app button down");
            testbtn.material.color = Color.green;
        }

    }

    // Update is called once per frame
    void Update()

    {
        GetMarkerctrbtn();

    }
}
