using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class buttontest : MonoBehaviour
{
    
    public GameObject ButtonTest;

    

    // Update is called once per frame
    void Update()
    {
        //列出所有输入设备，根据XR节点获取输入设备，必须先获取到输入设备，再获取对应输入设备的输入键值
        var controllerlist = new List<InputDevice>();

        bool triggerValue, gripValue;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, controllerlist);

        HapticCapabilities capabilities;

        if (controllerlist.Count == 1)
        {
            InputDevice rightcontroller = controllerlist[0];
            //Trigger键按下后将objecttest对象设置为非激活状态(隐藏)，并发送振动数据，使手柄振动
            if (rightcontroller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                ButtonTest.SetActive(!ButtonTest.activeInHierarchy);
                

                if (rightcontroller.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        //设置振动幅度为0.5，振动时长为0.5秒
                        uint channel = 0;
                        float amplitude = 0.5f;
                        float duration = 0.5f;
                        rightcontroller.SendHapticImpulse(channel, amplitude, duration);
                    }
                }
            }
            //Grip键按下后将objecttest对象设置为激活状态，并发送振动数据，使手柄振动
            if(rightcontroller.TryGetFeatureValue(CommonUsages.gripButton, out gripValue)&& gripValue)
            {
                ButtonTest.SetActive(!ButtonTest.activeInHierarchy);
                if (rightcontroller.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        //设置振动幅度为0.5，振动时长为1秒
                        uint channel = 0;
                        float amplitude = 0.5f;
                        float duration = 1.0f;
                        rightcontroller.SendHapticImpulse(channel, amplitude, duration);
                    }
                }
            }
            

            }


        }


    }

