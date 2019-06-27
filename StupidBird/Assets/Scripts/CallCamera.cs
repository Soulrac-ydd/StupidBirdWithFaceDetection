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

    [StructLayout(LayoutKind.Explicit)]
    public struct Color32Array
    {
        [FieldOffset(0)]
        public byte[] byteArray;

        [FieldOffset(0)]
        public Color32[] colors;
    }

    // Use this for initialization
    void Awake()
    {
        m_material = this.GetComponent<MeshRenderer>().material;
        //StartCoroutine("start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Start()
    {
        //获取授权  
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            webcamTex = new WebCamTexture(deviceName, 1080, 1920, 10);
            webcamTex.Play();
            m_material.mainTexture = webcamTex;


            //Texture2D t = new Texture2D(555, 913, TextureFormat.ARGB32, true);
            //t.ReadPixels(new Rect(0, 0, 555, 913), 0, 0, false);
            //byte[] bytes = t.EncodeToPNG();
            //string result = OfflineFaceDetection.Program.GetEyeStatus(bytes);

            //Debug.Log(result);
        }
        else
        {
        }
        //StartCoroutine("SeriousPhotoes");
    }
}
