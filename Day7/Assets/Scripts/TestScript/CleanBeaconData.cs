using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ximmerse.XR.Tag;
using static Ximmerse.XR.Tag.TagProfileLoading;

public class CleanBeaconData : MonoBehaviour
{
    private void OnMouseDown()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
    // Update is called once per frame
    void Update()
    {
        CleanTracking();
       
    }
    public void CleanTracking()
    {
        TagProfileLoading.Instance.CleanBeaconData();
    }
}
