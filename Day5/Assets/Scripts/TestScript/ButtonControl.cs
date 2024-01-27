using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{

    public GameObject Model;

    public GameObject Instruction;

    public GameObject ballPrefab;

    public GameObject startpoint;

    private MeshRenderer thisrender;

    public Text axisOutput;

    public Text triggerForceUI;

    public float speed = 0.1f;


    private void Start()
    {
        thisrender = Model.GetComponent<MeshRenderer>();

        Debug.Log("test");

    }

    // Update is called once per frame
    void Update()
    {
        //�г����������豸������XR�ڵ��ȡ�����豸�������Ȼ�ȡ�������豸���ٻ�ȡ��Ӧ�����豸�������ֵ
        var rightDevice = new List<InputDevice>();

        var leftdevice = new List<InputDevice>();

        bool menuValue, gripValue, triggerValue, primaryValue, secondaryValue, menuValueleft;

        float triggerForce;

        Vector2 joystick;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightDevice);

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftdevice);

        if (rightDevice.Count != 0 && leftdevice.Count != 0)
        {
            InputDevice rightcontroller = rightDevice[0];

            InputDevice leftcontroller = leftdevice[0];

            //�������ֱ��˵�������л�ģ����ɫ
            if (rightcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValue) && menuValue & Model.activeInHierarchy)
            {
                float h = Random.Range(0f, 1f );
                float s = Random.Range(0f, 1f);
                thisrender.material.color = Color.HSVToRGB(h, s,1);
            }
            //ͨ�����ֱ�A���Ŵ�ģ��
            if(rightcontroller.TryGetFeatureValue(CommonUsages.primaryButton, out primaryValue)&&primaryValue & Model.activeInHierarchy)
            {
                float oldsize = Model.transform.localScale.x;
                float newsize = 1.025f * oldsize;

                if(newsize < 1.5)
                {
                    Model.transform.localScale = new Vector3(newsize, newsize, newsize);
                }   
             }
            //ͨ�����ֱ�B����Сģ��
            if(rightcontroller.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue)&& secondaryValue & Model.activeInHierarchy)
            {
                float oldsize = Model.transform.localScale.x;
                float newsize = oldsize/1.025f;

                if(newsize > 0.1)
                {
                    Model.transform.localScale = new Vector3(newsize, newsize, newsize);
                }
                
            }
            //�������ֱ�Trigger�������Զ�����С��
            if (leftcontroller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Instantiate(ballPrefab, startpoint.transform.position, startpoint.transform.rotation);
            }
            //�������ֱ�Grip������ģ����Y����ת��ת
            if (leftcontroller.TryGetFeatureValue(CommonUsages.gripButton, out gripValue)&& gripValue & Model.activeInHierarchy)
            {
                Model.transform.eulerAngles += new Vector3(0,5,0);
            }
            //ͨ������ҡ�˿���ģ���ƶ�
            if (leftcontroller.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick) && joystick.x > 0.5)
            {
                Model.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (leftcontroller.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick) && joystick.x < -0.5)
            {
                Model.transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (leftcontroller.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick) && joystick.y > 0.5)
            {
                Model.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (leftcontroller.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick) && joystick.y < -0.5)
            {
                Model.transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            //ͨ�����ֱ��˵�����������ֻҪUI
            if(leftcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValueleft) && menuValueleft)
            {
                Instruction.SetActive(!Instruction.activeInHierarchy);

            }
           
            //��ʾҡ�˵�Vector2����,�Լ����ֱ�������µ�����ֵ
            axisOutput.text = $"ҡ�����ݣ�{joystick}";

            rightcontroller.TryGetFeatureValue(CommonUsages.trigger, out triggerForce);

            triggerForceUI.text = $"��������ȣ�{triggerForce}";

        }

    }



}
