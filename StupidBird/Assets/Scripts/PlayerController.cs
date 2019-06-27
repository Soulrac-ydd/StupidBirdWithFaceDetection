using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody2D;
    Vector3 upForward = new Vector3(0f, 0f, 30f);
    Vector3 downForward = new Vector3(0f, 0f, -90f);
    Camera screenCamera;

    FileStream file;
    BinaryWriter writer;

    string result;

    // Use this for initialization
    void Awake()
    {
        screenCamera = Camera.main;
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        StartCoroutine(SeriousPhotoes());
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
        file.Dispose();
        writer.Dispose();
        DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Adonis\Desktop\BirdScreenShot");
        directory.Delete(true);
        Debug.Log("Oooops");
        //Time.timeScale = 0;
        //SceneManager.LoadSceneAsync(0);
    }

    IEnumerator SeriousPhotoes()
    {
        //Color32Array colorArray = new Color32Array();
        //colorArray.colors = new Color32[webcamTex.width * webcamTex.height];
        //webcamTex.GetPixels32(colorArray.colors);
        //string result = OfflineFaceDetection.Program.GetEyeStatus(colorArray.byteArray);
        int pngNum = 1;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Texture2D t = new Texture2D(screenCamera.pixelWidth, screenCamera.pixelHeight, TextureFormat.ARGB32, true);
            t.ReadPixels(new Rect(0, 0, screenCamera.pixelWidth, screenCamera.pixelHeight), 0, 0, false);
            byte[] bytes = t.EncodeToPNG();
            file = File.Open(@"C:\Users\Adonis\Desktop\BirdScreenShot\" + pngNum + ".png", FileMode.Create);
            writer = new BinaryWriter(file);
            writer.Write(bytes);
            file.Close();
            writer.Close();
            Texture2D.DestroyImmediate(t);
            t = null;
            result = OfflineFaceDetection.Program.GetEyeStatus(pngNum);
            Debug.Log(result);
            pngNum++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
