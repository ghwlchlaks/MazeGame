using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine.UI;

public class WinboxCollider : MonoBehaviour {

	// Use this for initialization
	GameObject Player;
	public Text time;

	private void Start()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnControllerColliderHit(ControllerColliderHit playerCollider)
	{
		string text;
		string[] split_text;

		int minutes, seconds;

		if (playerCollider.collider.tag == "winbox") {
			text = time.text;
			split_text = text.Split(' ');
			minutes =Convert.ToInt32(split_text[2].Split(':')[0]);
			seconds = Convert.ToInt32(split_text[2].Split(':')[1]);

			string bestTime;

			if (PlayerPrefs.HasKey ("Stage" + Application.loadedLevelName)) { //스테이지의 시간이 저장되어있다면 시간 비교
				string load_time = PlayerPrefs.GetString ("Stage" + Application.loadedLevelName);
				int load_minutes = Convert.ToInt32 (load_time.Split (':') [0]);
				int load_seconds = Convert.ToInt32 (load_time.Split (':') [1]);


			
				if (load_minutes < minutes) {
					bestTime = Convert.ToString (load_minutes + ":" + load_seconds);
					Debug.Log ("시간 단축 변함 없음" + bestTime);
					PlayerPrefs.SetString ("Stage" + Application.loadedLevelName, bestTime);
				} else if (load_minutes == minutes) { //앞자리가 같으면 뒷자리 비교
					if (load_seconds <= seconds) { //load 한것이 뒷자리가 같거나 작으면 변함없음 
						bestTime = Convert.ToString (load_minutes + ":" + load_seconds);
						Debug.Log ("시간 단축 변함 없음" + bestTime);
						PlayerPrefs.SetString ("Stage" + Application.loadedLevelName, bestTime);
					} else {  //앞자리는 같고 뒷자리가 단축됐으면 신기록 
						bestTime = Convert.ToString (minutes + ":" + seconds);
						Debug.Log ("단축 됨" + bestTime);
						PlayerPrefs.SetString ("Stage" + Application.loadedLevelName, bestTime);
					}
				} else {  //load_minutes >minutes 앞자리부터 단축됨 신기록 저장
					bestTime = Convert.ToString (minutes + ":" + seconds);
					Debug.Log ("단축 됨" + bestTime);
					PlayerPrefs.SetString ("Stage" + Application.loadedLevelName, bestTime);
				}
			} else //스테이지의 시간 데이터가 없다면 지금 기록이 신기록 동시에 stagemanager에서 다음 스테이지가 열림
			{
				bestTime = Convert.ToString (minutes + ":" + seconds);
				Debug.Log ("다음 스테이지 오픈 " + bestTime);
				PlayerPrefs.SetString ("Stage" + Application.loadedLevelName, bestTime);
			}

			//시간 저장후 일단 스테이지 선택창으로이동 
			//나중에 clear 창이나 효과 생성
			SceneManager.LoadScene("MainScean");
		}
	}
}
