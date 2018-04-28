using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	bool isPause = true;
	public GameObject Pause_panel;
	public GameObject Pause_Btn;

	// Use this for initialization
	void Start () {
		Pause_panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	public void pause()
	{
		if (isPause) {//일시정지 버튼이 눌렸다면 
			Time.timeScale =0;
			Pause_panel.SetActive (true);
			Pause_Btn.SetActive (false);
			isPause = false;
		}
		else 
		{
			Time.timeScale = 1.0f;
			Pause_panel.SetActive (false);
			Pause_Btn.SetActive (true);
			isPause = true;
		}	
	}
	public void ToMain()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("MainScean");
	}
}
