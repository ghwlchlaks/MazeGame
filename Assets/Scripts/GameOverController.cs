using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
    public Canvas GameOver;
    float time = 0f;
    float alpha = 5.0f;

    public Button ReStart_Btn;
    public Button ToMain_Btn;

 	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        
       GameOver.GetComponent<CanvasGroup>().alpha = time/alpha;

        ReStart_Btn.onClick.AddListener(() => Restart());
        ToMain_Btn.onClick.AddListener(() => ToMain());
    }

    private void Restart()
    {
        stageManager.nextLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
    }
    private void ToMain()
    {
        stageManager.nextLevel = "MainScean";
        SceneManager.LoadScene("LoadingScene");
    } 
}
