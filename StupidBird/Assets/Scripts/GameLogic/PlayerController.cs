using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody2D;
    Vector3 upForward = new Vector3(0f, 0f, 30f);
    Vector3 downForward = new Vector3(0f, 0f, -90f);

    Texture2D tex;
    string result;
    byte[] bytes;
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);

    string[] eye_status;


    //上一帧的眼睛闭合程度，默认为1，表示完全睁开。
    double left_eye_pre = 1;
    double right_eye_pre = 1;

    //当前帧的眼睛闭合程度。
    double left_eye = 1;
    double right_eye = 1;

    //眨眼前后变化的阈值
    public double threshold = 0.5f;

    // Use this for initialization
    void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //眨眼控制
        //if(left_eye-left_eye_pre >= threshold && right_eye-right_eye_pre >= threshold)
        //{
        //    ......
        //}

        //使用鼠标左键控制
        if (Input.GetMouseButtonDown(0))
        {
            //m_rigidbody2D.AddForce(force);
            Vector2 vel = m_rigidbody2D.velocity;
            m_rigidbody2D.velocity = new Vector2(vel.x, 666f);
            //this.transform.Rotate(upForward, Space.World);
            this.transform.rotation = Quaternion.Euler(upForward);
            return;
        }
        Quaternion quaternion = Quaternion.Euler(downForward);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Oooops");
        Time.timeScale = 0;
        //SceneManager.LoadSceneAsync(0);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); //由于该截图处理在Start内，因此先等待0.5秒让其他的游戏逻辑处理执行完毕。
        int width = Screen.width;
        int height = Screen.height;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            left_eye_pre = left_eye;
            right_eye_pre = right_eye;
            //此处下面的0.77f和0.23f是为了控制截图的大小，未做自适应。本项目的分辨率默认为1080*1920。
            tex = new Texture2D(width, (int)(height * 0.77f), TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, height * 0.23f, width, height * 0.77f), 0, 0, false);
            //tex.Compress(false);
            tex.Apply();
            bytes = ImageConversion.EncodeToJPG(tex, 15);//第二个参数是图片的质量，一般为50，范围为0-100。此处选择15是为了降低图片的大小从而减少转换成Base64后所占的字节数。
            DestroyImmediate(tex);
            tex = null;
            result = OfflineFaceDetection.GetEyeStatus(bytes); //调用Http SDK接口
            bytes = null;
            //返回结果格式：“左眼闭合程度 | 右眼闭合程度”
            if (result != null)
            {
                eye_status = result.Split('|');
                left_eye = Convert.ToDouble(eye_status[0]);
                right_eye = Convert.ToDouble(eye_status[1]);
            }
            yield return waitForSeconds;
        }
    }
}
