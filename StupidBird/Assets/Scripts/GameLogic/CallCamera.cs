using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

public class CallCamera : MonoBehaviour
{
    public Material m_material;

    public string deviceName;

    //接收返回的图片数据
    WebCamTexture webcamTex;

    // Use this for initialization
    void Awake()
    {
        m_material = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Start()
    {
        //调用设备的摄像头
        //获取授权  
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            webcamTex = new WebCamTexture(deviceName, 1080, 1920, 10);
            m_material.mainTexture = webcamTex;
            webcamTex.Play();
        }
        else
        {
        }
    }
}
