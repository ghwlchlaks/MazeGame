using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_sight : MonoBehaviour {
	public GameObject sight_object;
	public GameObject Monster;
	public GameObject Player;
	public GameObject Respawn;

	//Monster_collider monster_class;
	bool monster_wall_enter;

	public bool sight_wall_enter;
	public bool sight_player_enter;

	private float Monster_speed;
	// Use this for initialization
	void Start () {
		monster_wall_enter = GameObject.FindGameObjectWithTag ("Monster").GetComponent<Monster_collider> ().monster_wall_enter;
		Monster_speed = 5.0f;
		//monster_class.monster_wall_enter = false;
		//Debug.Log (monster_class.monster_wall_enter);
		Debug.Log (monster_wall_enter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Wall")
		{
			sight_wall_enter = true;

		}
		if (col.tag == "Player") 	
		{
			sight_player_enter = true;
		}
	}
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Wall") {
			Debug.Log ("stay - wall-sightObject");
		} else if (col.tag == "Player" && !monster_wall_enter) {//!monster_class.monster_wall_enter 
			Debug.Log ("stay - Player - sightObject");
			Monster.transform.LookAt (Player.transform);//플레이어 쪽으로 방향을 튼다. 
			Monster.transform.Translate (Vector3.forward * Time.deltaTime * Monster_speed, Space.Self);
		} else if (col.tag == "Player" && monster_wall_enter) {

		} else {

			Monster.transform.LookAt (Respawn.transform);
			Monster.transform.Translate (Vector3.forward * Monster_speed * Time.deltaTime, Space.Self);
		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Wall") 
		{
			sight_wall_enter= false;
		}
		if (col.tag == "Player") 
		{
			sight_player_enter = false;
		}
	}
}
