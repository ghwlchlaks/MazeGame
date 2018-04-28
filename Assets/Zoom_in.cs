using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom_in : MonoBehaviour {
	public Image image;
	float zoom_in_speed;

	// Use this for initialization
	void Start () {
		zoom_in_speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		image.transform.localScale = new Vector3 (image.transform.localScale.x+zoom_in_speed, image.transform.localScale.y + zoom_in_speed, image.transform.localScale.z+zoom_in_speed);
		zoom_in_speed+=0.1f;
	}
}
