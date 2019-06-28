using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class OnlineFaceDetection
{
    // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
    // 返回token示例
    public static string access_token = "";

    // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
    private static string clientId = "7GLoNSeBYpVq5XYALtcF4e2E";
    // 百度云中开通对应服务应用的 Secret Key
    private static string clientSecret = "N8xZQGf8GgMqDQXorbeLSr4YBQfoRgAp";

    public static void SetAccessToken()
    {
        string authHost = "https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id=7GLoNSeBYpVq5XYALtcF4e2E&client_secret=N8xZQGf8GgMqDQXorbeLSr4YBQfoRgAp";
        HttpClient client = new HttpClient(); 
        List<KeyValuePair<string, string>> paraList = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        };

        HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
        string jsonClientStr = response.Content.ReadAsStringAsync().Result;
        JsonClientData jd = JsonConvert.DeserializeObject<JsonClientData>(jsonClientStr);
        access_token = jd.access_token;
    }

    public static string DetectDemo(string base64String)
    {
        string host = "https://aip.baidubce.com/rest/2.0/face/v3/detect?access_token=" + access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "post";
        request.KeepAlive = true;
        string str = "{\"image\":\"" + base64String + "\"," +
                        "\"image_type\":\"BASE64\",\"face_field\":\"age,beauty,eye_status\",\"max_face_num\":1,\"face_type\":\"LIVE\",\"liveness_control\":\"LOW\"}";
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        return result;
    }

    public static string GetEyeStatus(byte[] bytes)
    {
        SetAccessToken();
        string base64String = Convert.ToBase64String(bytes);
        string jsonStringResult = DetectDemo(base64String);
        JsonFaceRequestData jFRD = JsonConvert.DeserializeObject<JsonFaceRequestData>(jsonStringResult);
        try
        {
            double left_eye = jFRD.result.face_list[0].eye_status.left_eye;
            double right_eye = jFRD.result.face_list[0].eye_status.right_eye;

            return left_eye.ToString() + "||" + right_eye.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }
}
