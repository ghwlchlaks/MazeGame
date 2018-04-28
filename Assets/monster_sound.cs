using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_sound : MonoBehaviour {
//	GameObject[] enemy;
	public AudioSource source;
	public GameObject player;

	// Use this for initialization
	void Start () {
//		enemy = GameObject.FindGameObjectsWithTag("enemy");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (Vector3.Distance (player.transform.position, transform.position));
		if (Vector3.Distance (player.transform.position, transform.position) > 100) 
		{
			source.Play ();
		} 

//		for (int i = 0; i < enemy.Length; i++) 
//		{
//			if (Vector3.Distance (transform.position, enemy[i].transform.position) < 100.0) 
//			{
//				Debug.Log (enemy [i].name+" "+Vector3.Distance (transform.position, enemy[i].transform.position) );
//				source.Play ();
//
//			}
//		}


	}
}
