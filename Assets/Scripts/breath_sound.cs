using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breath_sound : MonoBehaviour {
	public AudioSource source;
	public Slider breathe_slider;

	float d_time;
	float breathe_time;

	// Use this for initialization
	void Start () {
		d_time = 0.0f;
		breathe_time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			source.Play ();
		}


		if (Input.GetKey (KeyCode.Space)) {
			if (source.pitch < 1.3f) {
				source.pitch += 0.002f;
				breathe_time += Time.deltaTime;
			}
		} 
		else if (source.pitch != 1.0f) 
		{
			if (source.pitch > 1.0f) {
				source.pitch -= 0.002f;
				d_time += Time.deltaTime * 1.5f;
			}
		} 

		if (breathe_slider.value == 101 || breathe_time<d_time) 
		{
			source.pitch = 1.0f;
			source.Stop();
			breathe_time = 0.0f;
			d_time = 0.0f;
		}

	}
}
