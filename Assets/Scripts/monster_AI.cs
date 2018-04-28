using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_AI : MonoBehaviour {
	//기본 변수
	public GameObject monster;
	public GameObject player;
	public GameObject Respawn;
	public GameObject Respawn1;
	public GameObject monster_object;


	public GameObject Floor;

	public Animator ani;

	//monster ai 구현 변수
	RaycastHit[] hit;
	float monster_speed;
	// Use this for initialization
	float delay_time = 10.0f;
	float time;
	bool isReturn;
	bool Respawn_complete;
	bool Respawn1_complete;
	bool detect_player;
	float attack_delay = 1.8f;
	float attack_time;
	float hp_delay=1.3f;
	float hp_time;

    //game over fade in 구현
    public GameObject fadeImage;

	//monster 시야 변수
    public Camera main_camera;
    public GameObject camera_sight;

	//피튀김 효과 변수
	GameObject blood_camera;
	BloodRainCameraController blood_controller;

	//sound 효과 변수
	AudioSource audio_source;
	public AudioClip audio_clip;
	bool attack_sound;

	void Start () {
		monster_speed = 7.0f;
		isReturn = false;
		Respawn_complete = true;
		Respawn1_complete = false;
		detect_player = false;
		//monster.transform.position = Respawn.transform.position;
		monster_object.transform.position =Respawn.transform.position;
        fadeImage.SetActive(false);
		blood_camera = GameObject.FindGameObjectWithTag ("blood_camera");
		blood_controller = blood_camera.GetComponent<BloodRainCameraController> ();
		audio_source = GetComponent<AudioSource> ();
		attack_sound = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//리스폰 0 1 사이 왕복 
		if (Respawn_complete&&!Respawn1_complete&&!detect_player) 
		{
			monster_speed = 12.0f;
			isReturn = false;
			monster_object.transform.LookAt (Respawn1.transform);
//			Quaternion qu1 = Quaternion.LookRotation(Respawn1.transform.position);
//			monster_object.transform.rotation = Quaternion.Slerp (monster_object.transform.rotation, qu1, Time.deltaTime * 5.0f);
			monster_object.transform.position = Vector3.MoveTowards (monster_object.transform.position, Respawn1.transform.position, monster_speed * Time.deltaTime);

			ani.SetTrigger ("walk");

			//Debug.Log(Vector3.Distance(monster_object.transform.position,Respawn1.transform.position)+"   1");
			if(Vector3.Distance(monster_object.transform.position,Respawn1.transform.position)<0.5f)
			{
				Respawn1_complete=true;
				Respawn_complete = false;
			}
		}
		if (!Respawn_complete && Respawn1_complete&&!detect_player) 
		{
//			Quaternion qu = Quaternion.LookRotation(Respawn.transform.position);
//			monster_object.transform.rotation = Quaternion.Slerp (monster_object.transform.rotation, qu, Time.deltaTime * 5.0f);
			monster_speed=12.0f;

			monster_object.transform.LookAt (Respawn.transform);
			monster_object.transform.position = Vector3.MoveTowards (monster_object.transform.position, Respawn.transform.position, monster_speed * Time.deltaTime);

			ani.SetTrigger ("walk");

			//Debug.Log(Vector3.Distance(monster_object.transform.position,Respawn.transform.position)+"   2");
			if(Vector3.Distance(monster_object.transform.position,Respawn.transform.position)<0.5f)
			{
				Respawn_complete=true;	
				Respawn1_complete = false;
			}
		}


		//monster ai 
		hit = Physics.SphereCastAll (monster_object.transform.position, 10.0f, monster_object.transform.forward, 100.0f);
		 //모든 laycast object 충돌 파악.
		for (int i = 0; i < hit.Length; i++)   
		{
            if (hit[i].collider.tag == "Coll_Player")  //player와 lay가 충돌한다면 
            {
                
                detect_player = true;
                isReturn = false;
                //Debug.Log ("player와 충돌");
                time += Time.deltaTime;

                //Debug.Log(hit[i].distance);
				monster_speed = 17.0f;
                if (time < delay_time)   //delay_time 간 쫒아감
                {
					ani.SetBool("run",true);
                    monster_object.transform.LookAt(player.transform);
                    monster_object.transform.Translate(Vector3.forward * Time.deltaTime * monster_speed, Space.Self);

                }
                else  //delay_time 시간을 넘기면 리스폰지역을 바라보게함
                {
                    monster_object.transform.LookAt(Respawn.transform);
                    time = 0;
                    isReturn = true;
                }

				if (hit[i].distance < 40.0f)   //player와 monster거리가 5미만일때 게임 오버
				{
					monster_speed = 0;
                    //Debug.Log("game over");
					ani.SetBool("run",false);
					ani.SetTrigger("attack");
					//main_camera.transform.SetParent(monster.transform);
					//main_camera.transform.LookAt(monster_object.transform);
					attack_time += Time.deltaTime;	
					blood_controller.HP = 70;
					monster.transform.GetComponent<CapsuleCollider> ().isTrigger = true;

					if (attack_time > attack_delay)
					{
						hp_time += Time.deltaTime;
						if (attack_sound)
						{
							audio_source.PlayOneShot (audio_clip);		
						}
						attack_sound = false;


						if(hp_delay<hp_time)
						{
							
							main_camera.GetComponent<Rigidbody> ().isKinematic = false;  //카메라 떨구기 
							player.GetComponent<CharacterController> ().enabled = false;
							
                            //Debug.Log(main_camera.transform.localPosition.y);
                            if (main_camera.transform.localPosition.y < 2.0f) 
							{
								main_camera.GetComponent<Rigidbody> ().isKinematic = true;
                               // Debug.Log("efef");
                                play_fade_in ();
							}		
							main_camera.transform.LookAt (camera_sight.transform);
							blood_controller.HP = 40;
							//Destroy(player.gameObject);
						}
					}
						
				}
            }
		}
		if (isReturn)   //리스폰지역으로 돌아옴.
		{
			//Debug.Log ("리스폰지역으로 이동중");
			monster_speed = 12.0f;
			monster_object.transform.position = Vector3.MoveTowards (monster_object.transform.position, Respawn.transform.position, monster_speed * Time.deltaTime);
			ani.SetTrigger ("walk");
			//Debug.Log(Vector3.Distance(monster_object.transform.position,Respawn.transform.position)+"   return");
			if(Vector3.Distance(monster_object.transform.position,Respawn.transform.position)<0.5f)
				{
					detect_player=false;
				}
		} 
	}
    private void play_fade_in()
    {
        
        fadeImage.SetActive(true);           
    }


}
