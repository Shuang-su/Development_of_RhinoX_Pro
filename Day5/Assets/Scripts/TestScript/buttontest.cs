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
        //�г����������豸������XR�ڵ��ȡ�����豸�������Ȼ�ȡ�������豸���ٻ�ȡ��Ӧ�����豸�������ֵ
        var controllerlist = new List<InputDevice>();

        bool triggerValue, gripValue;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, controllerlist);

        HapticCapabilities capabilities;

        if (controllerlist.Count == 1)
        {
            InputDevice rightcontroller = controllerlist[0];
            //Trigger�����º�objecttest��������Ϊ�Ǽ���״̬(����)�������������ݣ�ʹ�ֱ���
            if (rightcontroller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                ButtonTest.SetActive(!ButtonTest.activeInHierarchy);
                

                if (rightcontroller.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        //�����񶯷���Ϊ0.5����ʱ��Ϊ0.5��
                        uint channel = 0;
                        float amplitude = 0.5f;
                        float duration = 0.5f;
                        rightcontroller.SendHapticImpulse(channel, amplitude, duration);
                    }
                }
            }
            //Grip�����º�objecttest��������Ϊ����״̬�������������ݣ�ʹ�ֱ���
            if(rightcontroller.TryGetFeatureValue(CommonUsages.gripButton, out gripValue)&& gripValue)
            {
                ButtonTest.SetActive(!ButtonTest.activeInHierarchy);
                if (rightcontroller.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        //�����񶯷���Ϊ0.5����ʱ��Ϊ1��
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

