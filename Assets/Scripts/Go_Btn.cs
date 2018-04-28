using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Go_Btn : MonoBehaviour , IPointerDownHandler,IPointerUpHandler{
	bool check = false;
	public PlayerController controller;

	public void OnPointerDown(PointerEventData ped)
	{
		check = true;
	}
	public void OnPointerUp(PointerEventData ped)
	{
		check = false;
	}
	private void Start()
	{
	}
	private void Update()
	{
			if (check) 
		{
				controller.Run_Btn (5);
		}
	}
}
