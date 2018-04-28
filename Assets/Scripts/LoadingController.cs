using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {
	AsyncOperation operation;
	bool isLoad=false;
	float loading_time= 0f;

	public Slider Loading_Slider;

	// Use this for initialization
	void Start () {
		
		string sceneName = stageManager.nextLevel;
		Debug.Log (sceneName);
		StartCoroutine (startload (sceneName));
	}
	
	// Update is called once per frame
	void Update () {
		loading_time += Time.deltaTime;
		//Loading_Slider.value = loading_time;

		if (loading_time >= 2) {
			operation.allowSceneActivation = true;
		}
	}
	private IEnumerator startload(string sceanName)
	{
		operation = SceneManager.LoadSceneAsync(sceanName);
		operation.allowSceneActivation = false;

		if (isLoad == false)
		{
			isLoad = true;
			while (operation.progress < 0.9f) 
			{
				//Loading_Slider.value = operation.progress;
				yield return true;
			}
		}
	}
}
