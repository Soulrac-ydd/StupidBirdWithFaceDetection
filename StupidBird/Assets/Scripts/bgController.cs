using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgController : MonoBehaviour
{

    public float mSpeed = 666f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.left * Time.deltaTime * mSpeed);
        if (transform.localPosition.x < -1000)
        {
            transform.localPosition = new Vector3(2000, transform.localPosition.y, transform.localPosition.z);
        }
	}
}
