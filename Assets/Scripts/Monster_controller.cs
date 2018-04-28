using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_controller : MonoBehaviour {
	public GameObject Monster;
	public GameObject Player;

	float monster_speed = 5.0f;
	bool isWall_Enter;
	bool isPlayer_Enter;
	float distance ;
	Vector3 last_position;


	// Use this for initialization
	void Start () 
	{
		isWall_Enter = false;
		isPlayer_Enter=true;
		last_position = Monster.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (isWall_Enter||!isPlayer_Enter) 
		{
			Monster.transform.LookAt (last_position);
			Monster.transform.Translate (Vector3.forward * Time.deltaTime * monster_speed, Space.Self);
			//리스폰 정해서 해당 리스폰 지역과 충돌시(trigger)if문 안돌아가게 . 
			//적 앞에 투명한 오브젝트 생성후 해당 오브젝트의 tigger판정시 적 이 player에 접근
			//player tigger에 벽이 닿으면 
			Debug.Log ("update");
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "wall") 
		{
			isWall_Enter = true;
		}
		if (col.tag == "Player") 
		{
			isPlayer_Enter = true;
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player"&&!isWall_Enter)  
		{
			Monster.transform.LookAt (Player.transform);//플레이어 쪽으로 방향을 튼다. 
			Monster.transform.Translate(Vector3.forward*Time.deltaTime*monster_speed,Space.Self);
			Debug.Log ("stay");
			//쫒아간다. 
		}
		if (col.tag == "wall") 
		{
	//		ToOriginPosition ();
			//원위치로 돌아간다.
		}

	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") 
		{
	//		ToOriginPosition ();
			isPlayer_Enter = false;
		}
		if (col.tag == "wall") 
		{
			isWall_Enter = false;
		}
	}
	private void ToOriginPosition()
	{
	}
}
