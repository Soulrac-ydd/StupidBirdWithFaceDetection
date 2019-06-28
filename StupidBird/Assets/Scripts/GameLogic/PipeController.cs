using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    //-100 - 500
	// Use this for initialization
	void Awake ()
    {
        CreateRandomPosition();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //管道控制 未做自适应 用于管道的重复利用
        this.transform.Translate(new Vector3(-345 * Time.deltaTime, 0, 0), Space.Self);
        if (transform.localPosition.x < -1500)
        {
            transform.localPosition = new Vector3(750, transform.localPosition.y, transform.localPosition.z);
            CreateRandomPosition();
        }
    }

    /// <summary>
    /// 自动生成随机位置的管道，未做自适应。
    /// </summary>
    public void CreateRandomPosition()
    {
        Vector3 temp = new Vector3(this.transform.localPosition.x, Random.Range(-100, 550), this.transform.localPosition.z);
        this.transform.localPosition = temp;
    }
}
