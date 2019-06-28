using Baidu.Aip.Face;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class OfflineFaceDetection
{
    // 设置APPID/AK/SK
    static string APP_ID = "16639806";
    static string API_KEY = "7GLoNSeBYpVq5XYALtcF4e2E";
    static string SECRET_KEY = "N8xZQGf8GgMqDQXorbeLSr4YBQfoRgAp";

    static Face client = new Face(API_KEY, SECRET_KEY);

    static string base64String = null;
    static string imageType = "BASE64";

    // 如果有可选参数
    static Dictionary<string, object> options = new Dictionary<string, object>{
        {"face_field", "age,beauty,eye_status"}, //landmark
        {"max_face_num", 1},
        {"face_type", "LIVE"},
        {"liveness_control", "LOW"}
    };

    static JObject jsonObjectResult;
    static JsonFaceRequestData jFRD;
    static Eye_status eye_Status;

    static double left_eye;
    static double right_eye;


    public static JObject DetectDemo(Face client, string base64String)
    {
        // 带参数调用人脸检测
        try
        {
            Debug.Log(base64String.Length);
            JObject result = client.Detect(base64String, imageType, options);
            return result;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public static string GetEyeStatus(byte[] bytes)
    {
        client.AppId = APP_ID;
        client.Timeout = 5000;  // 修改超时时间

        base64String = Convert.ToBase64String(bytes);
        jsonObjectResult = DetectDemo(client, base64String);
        if (jsonObjectResult == null)
        {
            return null;
        }

        try
        {
            jFRD = JsonConvert.DeserializeObject<JsonFaceRequestData>(jsonObjectResult.ToString());
            eye_Status = jFRD.result.face_list[0].eye_status;
            left_eye = eye_Status.left_eye;
            right_eye = eye_Status.right_eye;

            return left_eye.ToString() + "||" + right_eye.ToString();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }


        /*
         * 通过嘴唇距离判断
         * 
        
        JObject jAllPosition = jFRD.result.face_list[0].landmark150;

        //60
        double mouth_lip_upper_outer_6_x = Convert.ToDouble(jAllPosition["mouth_lip_upper_outer_6"]["x"]);
        double mouth_lip_upper_outer_6_y = Convert.ToDouble(jAllPosition["mouth_lip_upper_outer_6"]["y"]);
        Position mouth_lip_upper_outer_6 = new Position(mouth_lip_upper_outer_6_x, mouth_lip_upper_outer_6_y);

        //67
        double mouth_lip_upper_inner_6_x = Convert.ToDouble(jAllPosition["mouth_lip_upper_inner_6"]["x"]);
        double mouth_lip_upper_inner_6_y = Convert.ToDouble(jAllPosition["mouth_lip_upper_inner_6"]["y"]);
        Position mouth_lip_upper_inner_6 = new Position(mouth_lip_upper_inner_6_x, mouth_lip_upper_inner_6_y);

        //70
        double mouth_lip_lower_inner_6_x = Convert.ToDouble(jAllPosition["mouth_lip_lower_inner_6"]["x"]);
        double mouth_lip_lower_inner_6_y = Convert.ToDouble(jAllPosition["mouth_lip_lower_inner_6"]["y"]);
        Position mouth_lip_lower_inner_6 = new Position(mouth_lip_lower_inner_6_x, mouth_lip_lower_inner_6_y);

        //64
        double mouth_lip_lower_outer_6_x = Convert.ToDouble(jAllPosition["mouth_lip_lower_outer_6"]["x"]);
        double mouth_lip_lower_outer_6_y = Convert.ToDouble(jAllPosition["mouth_lip_lower_outer_6"]["y"]);
        Position mouth_lip_lower_outer_6 = new Position(mouth_lip_lower_outer_6_x, mouth_lip_lower_outer_6_y);

        //取平均值
        double y_AverageOf60and67 = (mouth_lip_upper_outer_6.y + mouth_lip_upper_inner_6.y) / 2;
        double y_AverageOf70and64 = (mouth_lip_lower_inner_6.y + mouth_lip_lower_outer_6.y) / 2;
        double distanceOfPixel = y_AverageOf70and64 - y_AverageOf60and67;
        Console.WriteLine("上嘴唇中部与下嘴唇中部的像素距离是：{0}", distanceOfPixel);
        Console.WriteLine("上嘴唇下部与下嘴唇上部的像素距离是：{0}", mouth_lip_lower_inner_6.y - mouth_lip_upper_inner_6.y);
        */
    }
}
