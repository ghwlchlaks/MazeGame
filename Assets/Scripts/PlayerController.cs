using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 20.0f;
	Vector3 movement;
	public Animator ani;
	public cameraController controller;

	public CharacterController character;

	float cameraSpeed;
	Vector3 rotate_camera;

	bool isfire = false;
	public GameObject fire;

	public GameObject walking_cube;

	//public AudioSource audio;
	public AudioSource breathe_audio;
	public Slider breathe_slider;

	private void Start()
	{
		cameraSpeed = 3.0f;
		//audio = GetComponent<AudioSource> ();

	}

	private void Update()
	{	
		ani.SetBool ("Run", false);
		rotate_camera = new Vector3 (0, Input.GetAxis ("Mouse X"), 0);
		transform.Rotate (rotate_camera * cameraSpeed*Time.timeScale );

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			character.Move (transform.forward.normalized * Time.deltaTime * moveSpeed * Time.timeScale);
			ani.SetBool ("Run", true);
			//audio.PlayOneShot (walking_sound);
		} 
		
		if (Input.GetKey (KeyCode.S) == true) {
			character.Move (-transform.forward.normalized * Time.deltaTime * moveSpeed* Time.timeScale);
			ani.SetBool ("Run", true);
		} 


		if (Input.GetKey (KeyCode.D) == true) {
			character.Move (transform.right.normalized * Time.deltaTime * moveSpeed* Time.timeScale);
			ani.SetBool ("Run", true);
		}

		if (Input.GetKey (KeyCode.A) == true) {
			character.Move (-transform.right.normalized * Time.deltaTime * moveSpeed* Time.timeScale);
			ani.SetBool ("Run", true);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			breathe_audio.Play ();
		} 

		if (Input.GetKey (KeyCode.Space)) {
			ani.speed = 1.7f;
			moveSpeed = 40.0f;
			breathe_slider.value -= 1.0f*Time.deltaTime*10.0f;

			if (breathe_slider.value == 0.0f) 
			{
				moveSpeed = 20.0f;
				breathe_audio.Stop ();
				ani.speed = 1.0f;
			}

		} else if(!(Input.GetKey (KeyCode.Space))&&breathe_slider.value<=100){
			breathe_slider.value += 1.0f*Time.deltaTime*10.0f;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			moveSpeed = 20.0f;
			ani.speed = 1.0f;
			breathe_audio.Stop ();
		}

//		if (Input.GetKeyUp (KeyCode.W) == true) 
//		{
//			Debug.Log ("up");
//			walking_cube.GetComponent<sound_controller> ().enabled = false;
//
//
//		}
//		if (Input.GetKeyUp (KeyCode.A) == true) 
//		{
//			walking_cube.GetComponent<AudioSource> ().enabled = false;
//
//		}
//		if (Input.GetKeyUp (KeyCode.S) == true) 
//		{
//			walking_cube.GetComponent<AudioSource> ().enabled = false;
//		}
//		if (Input.GetKeyUp (KeyCode.D) == true) 
//		{
//			walking_cube.GetComponent<AudioSource> ().enabled = false;
//		}
		if (Input.GetKeyDown (KeyCode.F) == true)
			isfire = !isfire;

		if (isfire) 
		{
			//fire.GetComponent<Torchelight>().IntensityLight = GUI.HorizontalSlider(new Rect(25, 50, 150, 30), fire.GetComponent<Torchelight>().IntensityLight, 0.0F, fire.GetComponent<Torchelight>().MaxLightIntensity,SkinSlider.horizontalSlider,SkinSlider.horizontalSliderThumb);
			fire.GetComponent<Torchelight>().IntensityLight=100.0f;
		}
		else 
		{
			fire.GetComponent<Torchelight> ().IntensityLight = 0f;
		}
	
			

	}
	public void run(float h,float v)
	{
		transform.Translate (Vector3.forward * v*moveSpeed*Time.deltaTime* Time.timeScale);
		transform.Translate (Vector3.right * h*moveSpeed*Time.deltaTime* Time.timeScale);
	}

	public void Run_Btn(float v)
	{

		ani.SetBool ("Run1",true);
		transform.Translate(Vector3.forward * v *moveSpeed*Time.deltaTime* Time.timeScale);
	}

}
