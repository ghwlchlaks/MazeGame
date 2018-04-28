using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorController : MonoBehaviour {

	public Animator animator;


	private void Start()
	{
		animator.GetComponent<Animator> ();


	}

	public void choice_StageBtn(int key)
	{
		Vector3 currentPosition = transform.localPosition;
		Debug.Log (currentPosition.y);
		switch (key) 
		{
		case 1: 			//easy  90 -420 0
			if (currentPosition.y <= 0 && currentPosition.y >= -100) 
			{
				animator.SetTrigger ("FromNormalToEasy");
			}
			//normal position ->easy position
			else if (currentPosition.y > 0) 
			{
				animator.SetTrigger ("FromHardToEasy");
				//hard poasition ->easy position 
			} 
			else {}
			break;
		case 2:				//normal  90 0 0 
			if (currentPosition.y < 0)
			{
				animator.SetTrigger ("FromEasyToNormal");
				//easy position->normal positinon
			} 
			else
			{
				animator.SetTrigger ("FromHardToNormal");
				//hard position ->normal position
				//	StagePanel.localPosition=new Vector3(90,0,0);
			}
			break;
		case 3:				//hard 90 420 0
			if (currentPosition.y >= 0)
			{
				animator.SetTrigger ("FromNormalToHard");
			//normal positinon ->hard position
			}
			else
			{
				animator.SetTrigger ("FromEasyToHard");
			//easy position -> hard position

			//StagePanel.localPosition=new Vector3(90,420,0);
			}
			break;
		}
	}

}
