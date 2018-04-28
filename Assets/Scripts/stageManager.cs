using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class stageManager : MonoBehaviour {

	public GameObject StageContainer;
	public GameObject stageprefab;

	public int Max_Stage;

	private float starttime = 0.0f;
	private float addtime = 0.2f;

	public static string nextLevel;

	private void Start()  
	{
		
		Sprite[] stages_easy = Resources.LoadAll<Sprite> ("Stage\\Easy");   //해당 폴더에 있는 sprite 타입을 모두 로드해라 . 
		Sprite[] stages_normal = Resources.LoadAll<Sprite> ("Stage\\Normal");
		Sprite[] stage_hard = Resources.LoadAll<Sprite> ("Stage\\Hard");
		int stage_name = 0;
		Debug.Log (stages_easy [0].name);
		foreach (Sprite stage in stages_easy)  //폴더에 sprite갯수만큼 반복
		{
			GameObject container = Instantiate (stageprefab) as GameObject;  //미리 지정해놓은 prefab(상태 지정)을 container라는 변수명으로 생성한다.
			container.GetComponent<Image> ().sprite = stage;      			//container에 이미지를 폴더에 지정한 이미지로 넣는다. 
			container.transform.SetParent (StageContainer.transform, false);	//container에 부모를 설정 미리 설정을 해놓은 stagecontainer를 부모로 설정

			string sceneName = stage.name;    //해당 폴더에 sprite타입을 가진것에 이름을 string형ㅡ로 저장
			container.name = stage.name;  //해당 폴더에 sprite타입을 가진것에 이름을 container에 이름으로 설정
			//string sceanName= stage_name.ToString();

			//container.name= stage_name.ToString();
			stage_name++;
			container.transform.GetChild (1).GetComponent<Text> ().text += stage_name;  


			container.GetComponent<Button> ().onClick.AddListener (() =>loadStage ("LoadingScene",sceneName));
			 //자물쇠에 몇스테이지인지 적기

		//container.GetComponent<Button> ().onClick.AddListener (() =>loadStage (sceanName));   //각각 container가 클릭되었을때 laodstage라ㄴ 이벤트 핸들러가 호출
		//container.GetComponent<Button> ().onClick.AddListener (() =>StartCoroutine( startload (sceanName)));  
			container.GetComponent<Button> ().enabled = false;    //container에 버튼 기능을 false로 설정 
		}

		foreach (Sprite stage in stages_normal) 
		{
			GameObject container = Instantiate (stageprefab)as GameObject;
			container.GetComponent<Image> ().sprite = stage;
			container.transform.SetParent (StageContainer.transform, false);

			string sceneName = stage.name;
			container.name = stage.name;
		//	string sceanName= stage_name.ToString();
		//	container.name= stage_name.ToString();
			stage_name++;

			container.GetComponent<Button> ().onClick.AddListener (() =>loadStage ("LoadingScene",sceneName));
			container.transform.GetChild (1).GetComponent<Text> ().text += stage_name;
		//	container.GetComponent<Button> ().onClick.AddListener (() => loadStage (sceanName));
			container.GetComponent<Button> ().enabled = false;
		}

		foreach (Sprite stage in stage_hard) 
		{
			GameObject container = Instantiate (stageprefab)as GameObject;
			container.GetComponent<Image> ().sprite = stage;
			container.transform.SetParent (StageContainer.transform, false);

		string sceneName = stage.name;
		container.name = stage.name;

		//	string sceanName= stage_name.ToString();
		//	container.name= stage_name.ToString();
		//	stage_name++;

			container.GetComponent<Button> ().onClick.AddListener (() =>loadStage ("LoadingScene",sceneName));
			stage_name++;
			container.transform.GetChild (1).GetComponent<Text> ().text += stage_name;
		//	container.GetComponent<Button> ().onClick.AddListener (() => loadStage (sceanName));
			container.GetComponent<Button> ().enabled = false;
		}

		StageContainer.transform.GetChild (0).GetComponent<Button> ().enabled = true; //첫번째 스테이지 오픈 
		StageContainer.transform.GetChild(0).GetChild(2).GetComponentInParent<Text>().text="";
	}

	private void Update()
	{
		openStage ();

		starttime += addtime;
	}

	private void loadStage(string loadingScene,string nextlevel)   //stage 씬 로드 
	{
		nextLevel = nextlevel;
		SceneManager.LoadScene (loadingScene);
	}


	private void openStage()
	{
		
		for (int i = 10; i < Max_Stage; i++) { //테스트겸 발표용 스테지이 오픈 기능
			PlayerPrefs.SetString("Stage"+i,"30:00");
		}

		for (int i = 0; i < StageContainer.transform.childCount-1; i++)  // -1을하는 이유는 exception 이 뜬다. 
		{
			int number = i + 10;
			if (PlayerPrefs.HasKey ("Stage" + number)) //전 stage의 키가 있다면 (시간이 저장되어있다면 ) 
			{
				StageContainer.transform.GetChild (i +1).GetComponent<Button> ().enabled = true; //다음 스테이지를 오픈한다. 
				//StageContainer.transform.GetChild (i).GetChild (0).GetComponent<Text> ().font = Resources.LoadAll("Fonts")[0] as Font;
				//StageContainer.transform.GetChild (i).GetChild (0).GetComponent<Text> ().color = new Color32(140, 36 ,35,255);
				StageContainer.transform.GetChild(i).GetChild(0).GetComponent<Text>().text=" CLEAR";

				StageContainer.transform.GetChild(i).GetChild(2).GetComponent<Text>().text="";   //클리어한곳과 현재 스테이지 x없애기 
				StageContainer.transform.GetChild(i+1).GetChild(2).GetComponent<Text>().text="";
			
			}
		}
	}
}
