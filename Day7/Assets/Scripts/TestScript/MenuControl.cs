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
        //�г����������豸������XR�ڵ��ȡ�����豸�������Ȼ�ȡ�������豸���ٻ�ȡ��Ӧ�����豸�������ֵ
        var controllerlist = new List<InputDevice>();

        bool menuValue;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, controllerlist);


        if (controllerlist.Count == 1)
        {
            InputDevice rightcontroller = controllerlist[0];

            bool currentButton = rightcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValue);
            //���ֱ��˵������µ�����������ָ���˵�
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

