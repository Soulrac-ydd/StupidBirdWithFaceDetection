using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{

    public float mSpeed = 666f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //背景控制 未做自适应 用于背景的重复利用 对应Unity中Hierarchy三个disable的bg 使用人脸检测则不需要背景 
        transform.Translate(Vector3.left * Time.deltaTime * mSpeed);
        if (transform.localPosition.x < -1000)
        {
            transform.localPosition = new Vector3(2000, transform.localPosition.y, transform.localPosition.z);
        }
	}
}
