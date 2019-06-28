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

    // Use this for initialization
    void Awake()
    {
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
        yield return new WaitForSeconds(0.5f);
        //Color32Array colorArray = new Color32Array();
        //colorArray.colors = new Color32[webcamTex.width * webcamTex.height];
        //webcamTex.GetPixels32(colorArray.colors);
        //string result = OfflineFaceDetection.Program.GetEyeStatus(colorArray.byteArray);
        int width = Screen.width ;
        int height = Screen.height;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            tex = new Texture2D(width, (int)(height * 0.77f), TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, height * 0.23f, width, height * 0.77f), 0, 0, false);
            //tex.Compress(false);
            tex.Apply();
            bytes = ImageConversion.EncodeToJPG(tex, 15);
            DestroyImmediate(tex);
            tex = null;
            result = OfflineFaceDetection.GetEyeStatus(bytes);
            bytes = null;
            Debug.Log(result);
            yield return waitForSeconds;
        }
    }
}
