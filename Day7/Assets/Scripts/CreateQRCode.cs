using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

/// <summary>
/// ������ά��
/// </summary>
public class CreateQRCode : MonoBehaviour
{

    //��Ŷ�ά�������ͼƬ
    Texture2D encoded;

    [Header("��Ҫ������ά����ַ�")]
    public string QrCodeStr = "https://www.baidu.com/";
    [Header("����Ļ����ʾ��ά�� ")]
    public RawImage rawImg;

    void Start()
    {
        /*��ʼ������ͼƬ
         * ע�⣺��߶ȴ�С������256��
         * �������������������߽����
         */
        encoded = new Texture2D(256, 256);
        CreatQr(); //�������ɶ�ά��
    }

    #region ���ɶ�ά��

    /// <summary>  
    /// ������ά��
    /// </summary>  
    public void CreatQr()
    {
        if (QrCodeStr != string.Empty)
        {
            //��ά��д��ͼƬ    
            var color32 = Encode(QrCodeStr, encoded.width, encoded.height);
            encoded.SetPixels32(color32); //���������������ɫ
            encoded.Apply();
            //���ɵĶ�ά��ͼƬ����RawImage    
            rawImg.texture = encoded;
        }
        else
            Debug.Log("û��������Ϣ");
    }

    /// <summary>
    /// ���ɶ�ά�� 
    /// </summary>
    /// <param name="textForEncoding">��Ҫ������ά����ַ���</param>
    /// <param name="width">��</param>
    /// <param name="height">��</param>
    /// <returns></returns>       
    private static Color32[] Encode(string formatStr, int width, int height)
    {

        //���ƶ�ά��ǰ����һЩ����
        QrCodeEncodingOptions options = new QrCodeEncodingOptions();

        //�����ַ���ת����ʽ��ȷ���ַ�����Ϣ������ȷ
        options.CharacterSet = "UTF-8";

        //���û�������Ŀ�Ⱥ͸߶ȵ�����ֵ
        options.Width = width;
        options.Height = height;

        //���ö�ά���Ե���׿�ȣ�ֵԽ�����׿�ȴ󣬶�ά��ͼ�С��
        options.Margin = 1;

        /*ʵ�����ַ������ƶ�ά�빤��
         * BarcodeFormat:�������ʽ
         * Options�� �����ʽ��֧�ֵı����ʽ��
         */
        var barcodeWriter = new BarcodeWriter { Format = BarcodeFormat.QR_CODE, Options = options };
        //���ж�ά����Ʋ����з���ͼƬ����ɫ������Ϣ
        return barcodeWriter.Write(formatStr);

    }
    #endregion
}

