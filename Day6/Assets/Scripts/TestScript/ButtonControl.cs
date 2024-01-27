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

    }

    // Update is called once per frame
    void Update()
    {
        //列出所有输入设备，根据XR节点获取输入设备，必须先获取到输入设备，再获取对应输入设备的输入键值
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

            //按下右手柄菜单键随机切换模型颜色
            if (rightcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValue) && menuValue & Model.activeInHierarchy)
            {
                float h = Random.Range(0f, 1f );
                float s = Random.Range(0f, 1f);
                thisrender.material.color = Color.HSVToRGB(h, s,1);
            }
            //通过右手柄A键放大模型
            if(rightcontroller.TryGetFeatureValue(CommonUsages.primaryButton, out primaryValue)&&primaryValue & Model.activeInHierarchy)
            {
                float oldsize = Model.transform.localScale.x;
                float newsize = 1.025f * oldsize;

                if(newsize < 1.5)
                {
                    Model.transform.localScale = new Vector3(newsize, newsize, newsize);
                }   
             }
            //通过右手柄B键缩小模型
            if(rightcontroller.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue)&& secondaryValue & Model.activeInHierarchy)
            {
                float oldsize = Model.transform.localScale.x;
                float newsize = oldsize/1.025f;

                if(newsize > 0.1)
                {
                    Model.transform.localScale = new Vector3(newsize, newsize, newsize);
                }
                
            }
            //按下左手柄Trigger按键后，自动生成小球
            if (leftcontroller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Instantiate(ballPrefab, startpoint.transform.position, startpoint.transform.rotation);
            }
            //按下左手柄Grip按键，模型延Y轴旋转旋转
            if (leftcontroller.TryGetFeatureValue(CommonUsages.gripButton, out gripValue)&& gripValue & Model.activeInHierarchy)
            {
                Model.transform.eulerAngles += new Vector3(0,5,0);
            }
            //通过左手摇杆控制模型移动
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
            //通过左手柄菜单键唤出操作只要UI
            if(leftcontroller.TryGetFeatureValue(CommonUsages.menuButton, out menuValueleft) && menuValueleft)
            {
                Instruction.SetActive(!Instruction.activeInHierarchy);

            }
           
            //显示摇杆的Vector2数据,以及右手柄扳机按下的力度值
            axisOutput.text = $"摇杆数据：{joystick}";

            rightcontroller.TryGetFeatureValue(CommonUsages.trigger, out triggerForce);

            triggerForceUI.text = $"扳机键力度：{triggerForce}";

        }

    }



}
