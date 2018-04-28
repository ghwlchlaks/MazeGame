using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameraController : MonoBehaviour {



	public Transform target;

	public float current_x{ set; get;}
	public float current_y{set;get;}
	public float desired_x{ set; get;}
	public float desired_y{set;get;}

	public float cameraSpeed{ set; get;}
	private Vector3 offset;
	public float distance = 20.0f;  //카메라와 player 사이에  거리(z)
	public float yOffset = 10.0f;	//카메라와 player 사이에  거리(y)


	Vector3 rotate_camera;

	private void Start()
	{
		cameraSpeed = 3.0f;
		offset = new Vector3 (0, yOffset, -1f * distance);
		target.GetComponent<Transform> ();
	}

	private void LateUpdate()
	{

//		if(Input.touchCount==1){ //터치했을때
//			Touch touch = Input.GetTouch (0);
//			if (touch.phase == TouchPhase.Began) {
//				current_x = touch.position.x;
//
//			}else if (touch.phase == TouchPhase.Moved) {
//				desired_x = touch.position.x;
//				transform.RotateAround (target.position,Vector3.up,(desired_x - current_x)*Time.deltaTime);
//				//target.transform.Rotate (Vector3.up , (desired_x-current_x) *Time.deltaTime,0) ;
//
//
//			}
//		
//		}

	
		rotate_camera = new Vector3 (-Input.GetAxis ("Mouse Y"), 0, 0);
		transform.Rotate (rotate_camera * cameraSpeed*Time.deltaTime * Time.timeScale);
	
		//transform.LookAt (target);
	}


}
