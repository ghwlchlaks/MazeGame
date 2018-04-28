using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_collider : MonoBehaviour {
	public GameObject Monster;
	public GameObject Player;
	public GameObject Respawn;

	public bool monster_player_enter;
	public bool monster_wall_enter;

	float Monster_speed;

	// Use this for initialization
	void Start () {
		monster_player_enter = false;
		monster_wall_enter = false;
		Monster_speed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			monster_player_enter = true;
		}
		if (col.tag == "Wall") {
			monster_wall_enter = true;
			Monster.transform.LookAt (Respawn.transform);
			Monster.transform.Translate (Vector3.forward * Monster_speed * Time.deltaTime, Space.Self);
		}
	}
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player") {

		}
		if (col.tag == "Wall") {
			
			Monster.transform.LookAt (Respawn.transform);
			Monster.transform.Translate (Vector3.forward * Monster_speed * Time.deltaTime, Space.Self);
		}

	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") {
			monster_player_enter = false;
		}
		if (col.tag == "Wall") {
			monster_wall_enter = false;
		}

	}
}
