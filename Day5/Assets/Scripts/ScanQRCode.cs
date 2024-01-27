using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


/// <summary>
/// 扫描图片
/// </summary>
public class ScanQRCode : MonoBehaviour

{
    bool isOpen = true; //true当前开启扫描状态 false 当前是关闭扫描状态

    Animator ani; //扫描动画

    private WebCamTexture m_webCameraTexture;//摄像头实时显示的画面
    private BarcodeReader m_barcodeRender; //申请一个读取二维码的变量

    [Header("显示摄像头画面的RawImage")]
    public RawImage m_cameraTexture;

    [Header("扫描间隔")]
    public float m_delayTime = 3f;

    [Header("开启扫描按钮")]
    public Button openScanBtn;

    public int camindex;

    public Text ScanResult;

    public Text ScanState;

    public GameObject ScanWindow;

    void Start()
    {
        //调用摄像头并将画面显示在屏幕RawImage上
        WebCamDevice[] tDevices = WebCamTexture.devices;    //获取所有摄像头
        string tDeviceName = tDevices[camindex].name;  //根据摄像头的索引调用摄像头，使用摄像头的画面生成图片信息，RhinoX Pro的摄像头index为2，PC上默认为0
        m_webCameraTexture = new WebCamTexture(tDeviceName, 400, 300);//名字,宽,高
        m_cameraTexture.texture = m_webCameraTexture;   //赋值图片信息
        m_webCameraTexture.Play();  //开始实时显示

        m_barcodeRender = new BarcodeReader();
        ani = GetComponent<Animator>();

        OpenScanQRCode(); //默认不扫描
        //按钮监听
        
        openScanBtn.onClick.AddListener(OpenScanQRCode);
        
    }

    #region 扫描二维码

    //开启关闭扫描二维码
    void OpenScanQRCode()
    {
        if (isOpen)
        {
            //开启状态，取消扫描
            ScanState.text = $"点击开启扫描";
            ani.Play("CloseScan", 0, 0);
            CancelInvoke("CheckQRCode");
            
        }
        else
        {
            //关闭状态，点击按钮开启扫描

            //开始扫描
            ani.Play("OpenScan", 0, 0);

            //以秒为单位调用方法 
            InvokeRepeating("CheckQRCode", 0, m_delayTime);

            ScanState.text = $"正在扫描......";
        }
        isOpen = !isOpen;
        Debug.Log(isOpen);
    }

    #endregion

    #region 检索二维码方法
    /// <summary>
    /// 检索二维码方法
    /// </summary>
    public void CheckQRCode()
    {
        //存储摄像头画面信息贴图转换的颜色数组
        Color32[] m_colorData = m_webCameraTexture.GetPixels32();

        //将画面中的二维码信息检索出来
        var tResult = m_barcodeRender.Decode(m_colorData, m_webCameraTexture.width, m_webCameraTexture.height);

        if (tResult != null)
        {

            ScanResult.text = $"恭喜你，扫描成功,扫描结果：{tResult.Text}";

            ScanWindow.SetActive(false);
            //Application.OpenURL(tResult.Text);
            Debug.Log(tResult.Text);
        }

    }
    #endregion

}