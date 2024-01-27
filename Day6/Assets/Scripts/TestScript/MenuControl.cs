using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MenuControl : MonoBehaviour
{
    
    
    private GameObject TextMenual;

    private GameObject TextStart;

    private GameObject VirtualHand;

    public GameObject UI;

    // Update is called once per frame
    void Update()
    {
        //列出所有输入设备，根据XR节点获取输入设备，必须先获取到输入设备，再获取对应输入设备的输入键值
        var controllerlist = new List<InputDevice>();

        bool menuValue;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, controllerlist);


        if (controllerlist.Count == 1)
        {
            InputDevice rightcontroller = controllerlist[0];

            bool currentButton = rightcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValue);
            //右手柄菜单键按下调出或者隐藏指引菜单
            if (currentButton && menuValue)
            {
                
                TextMenual.SetActive(!TextMenual.activeInHierarchy);
                TextStart.SetActive(false);

            }

        }
        
    }
    public void VirtualHandControl()
    {
        VirtualHand.SetActive(!VirtualHand.activeInHierarchy);
    }

    public void TextDisplay()
    {
        UI.SetActive(!UI.activeInHierarchy);
    }


}

