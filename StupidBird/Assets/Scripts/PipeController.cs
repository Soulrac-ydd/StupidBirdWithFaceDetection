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
        this.transform.Translate(new Vector3(-345 * Time.deltaTime, 0, 0), Space.Self);
        if (transform.localPosition.x < -1500)
        {
            transform.localPosition = new Vector3(750, transform.localPosition.y, transform.localPosition.z);
            CreateRandomPosition();
        }
    }

    public void CreateRandomPosition()
    {
        Vector3 temp = new Vector3(this.transform.localPosition.x, Random.Range(-100, 550), this.transform.localPosition.z);
        this.transform.localPosition = temp;
    }
}
