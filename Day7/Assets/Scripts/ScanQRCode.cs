using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


/// <summary>
/// ɨ��ͼƬ
/// </summary>
public class ScanQRCode : MonoBehaviour

{
    bool isOpen = true; //true��ǰ����ɨ��״̬ false ��ǰ�ǹر�ɨ��״̬

    Animator ani; //ɨ�趯��

    private WebCamTexture m_webCameraTexture;//����ͷʵʱ��ʾ�Ļ���
    private BarcodeReader m_barcodeRender; //����һ����ȡ��ά��ı���

    [Header("��ʾ����ͷ�����RawImage")]
    public RawImage m_cameraTexture;

    [Header("ɨ����")]
    public float m_delayTime = 3f;

    [Header("����ɨ�谴ť")]
    public Button openScanBtn;

    public int camindex;

    public Text ScanResult;

    public Text ScanState;

    public GameObject ScanWindow;

    void Start()
    {
        //��������ͷ����������ʾ����ĻRawImage��
        WebCamDevice[] tDevices = WebCamTexture.devices;    //��ȡ��������ͷ
        string tDeviceName = tDevices[camindex].name;  //��������ͷ��������������ͷ��ʹ������ͷ�Ļ�������ͼƬ��Ϣ��RhinoX Pro������ͷindexΪ2��PC��Ĭ��Ϊ0
        m_webCameraTexture = new WebCamTexture(tDeviceName, 400, 300);//����,��,��
        m_cameraTexture.texture = m_webCameraTexture;   //��ֵͼƬ��Ϣ
        m_webCameraTexture.Play();  //��ʼʵʱ��ʾ

        m_barcodeRender = new BarcodeReader();
        ani = GetComponent<Animator>();

        OpenScanQRCode(); //Ĭ�ϲ�ɨ��
        //��ť����
        
        openScanBtn.onClick.AddListener(OpenScanQRCode);
        
    }

    #region ɨ���ά��

    //�����ر�ɨ���ά��
    void OpenScanQRCode()
    {
        if (isOpen)
        {
            //����״̬��ȡ��ɨ��
            ScanState.text = $"�������ɨ��";
            ani.Play("CloseScan", 0, 0);
            CancelInvoke("CheckQRCode");
            
        }
        else
        {
            //�ر�״̬�������ť����ɨ��

            //��ʼɨ��
            ani.Play("OpenScan", 0, 0);

            //����Ϊ��λ���÷��� 
            InvokeRepeating("CheckQRCode", 0, m_delayTime);

            ScanState.text = $"����ɨ��......";
        }
        isOpen = !isOpen;
        Debug.Log(isOpen);
    }

    #endregion

    #region ������ά�뷽��
    /// <summary>
    /// ������ά�뷽��
    /// </summary>
    public void CheckQRCode()
    {
        //�洢����ͷ������Ϣ��ͼת������ɫ����
        Color32[] m_colorData = m_webCameraTexture.GetPixels32();

        //�������еĶ�ά����Ϣ��������
        var tResult = m_barcodeRender.Decode(m_colorData, m_webCameraTexture.width, m_webCameraTexture.height);

        if (tResult != null)
        {

            ScanResult.text = $"��ϲ�㣬ɨ��ɹ�,ɨ������{tResult.Text}";

            ScanWindow.SetActive(false);
            //Application.OpenURL(tResult.Text);
            Debug.Log(tResult.Text);
        }

    }
    #endregion

}