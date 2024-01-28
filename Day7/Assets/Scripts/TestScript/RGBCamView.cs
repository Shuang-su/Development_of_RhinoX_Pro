using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBCamView : MonoBehaviour
{
    
    

    // ͼƬ���
    public RawImage rawImage;

    //ͼ�������ʵ��
    public RectTransform imageParent;

    //��ǰ�������
    private int index = 2;

    //��ǰ���е����
    private WebCamTexture currentWebCam;
    void Start()
    {
        StartCoroutine(Call());
    }
    

    public IEnumerator Call()
    {
        // ����Ȩ��
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);


        //if (Application.HasUserAuthorization(UserAuthorization.WebCam) && WebCamTexture.devices.Length > 0)
        //{
            // ���������ͼ
            currentWebCam = new WebCamTexture(WebCamTexture.devices[index].name, 1920, 1080, 24);
            rawImage.texture = currentWebCam;
            currentWebCam.Play();

            //ǰ�ú�������ͷ��Ҫ��תһ���Ƕ�,�������ǲ���ȷ��,��������Play()������
            //rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, -currentWebCam.videoRotationAngle);
        //}
    }

    //�л�ǰ������ͷ
    public void SwitchCamera()
    {
        if (WebCamTexture.devices.Length < 1)
            return;

        if (currentWebCam != null)
            currentWebCam.Stop();

        index++;
        index = index % WebCamTexture.devices.Length;

        // ���������ͼ
        currentWebCam = new WebCamTexture(WebCamTexture.devices[index].name, 1920, 1080, 24);
        rawImage.texture = currentWebCam;
        currentWebCam.Play();

        //ǰ�ú�������ͷ��Ҫ��תһ���Ƕȣ��������ǲ���ȷ��,��������Play()������
        rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, -currentWebCam.videoRotationAngle);
    }
}