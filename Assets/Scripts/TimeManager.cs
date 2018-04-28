using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	public GameObject time_text;
	//public GUIText time_text;
	float timeCnt;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timeCnt += Time.deltaTime*Time.timeScale;
		string timeStr;
		timeStr = "" + timeCnt.ToString ("00.00");
		timeStr = timeStr.Replace (".",":");
		time_text.GetComponent<Text>().text = "Time : "+ timeStr;
	}
	void OnGUI()
	{
		string timeStr;
		timeStr = "" + timeCnt.ToString ("00.00");
		timeStr = timeStr.Replace (".",":");
		//time_text.text = "Time : " + timeStr;

	}
}
