using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_controller : MonoBehaviour {
	AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		audio = GetComponent<AudioSource> ();
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Floor") 
		{
			//Debug.Log ("walking");
			//AudioSource.PlayClipAtPoint (clip, transform.position);
			audio.Play();
		}

	}
}
