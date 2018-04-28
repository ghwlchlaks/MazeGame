using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject gameManager;


	private void Awake()
	{
	}

	private void Start()
	{
		Screen.SetResolution(Screen.width*3/2, Screen.width,  true); 
	}

	private void Update()
	{
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();			
			}
		}

	
	}

	public void SceanLoad()
	{

	Application.LoadLevel ("MainScean");
	}
}
