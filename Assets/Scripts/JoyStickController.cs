using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

	private Image Bg_img;
	private Image JoyStick_img;

	public  Vector3 inputVector{ set; get;} //공을 움직이게한다.

	private void Start()
	{
		Bg_img = GetComponent<Image> ();
		JoyStick_img = transform.GetChild (0).GetComponent<Image> ();
	}
	public virtual void OnDrag(PointerEventData ped) //bg_img영역에 터치가 발생 했을때 
	{
		Vector2 pos;
	
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (Bg_img.rectTransform, ped.position, ped.pressEventCamera, out pos)) {//터치된 로칼 좌표값을 pos에 할당
				//Debug.Log (pos.x + " " + pos.y);
				pos.x = (pos.x / Bg_img.rectTransform.sizeDelta.x);
				pos.y = (pos.y / Bg_img.rectTransform.sizeDelta.y);  //입력된 좌표를 sizedeta 값(100)으로 나누어 -1~0(x) 0~1(y) 사이의 값으로 변환합니다.
				//	Debug.Log (pos.x + " " + pos.y);

				inputVector = new Vector3 (pos.x, 0, pos.y);  
				//inputVector = new Vector3 (pos.x * 2 + 1, pos.y * 2 - 1, 0);  //-1~1 사이의값으로 만듬으로써 상하 좌우를 구분한다. 
				// debug결과 바로 위 코드를 실행하지 않아도 -1~1 사이의값으로 된다 중앙이 0,0으로 되어있어서 그런듯.. 
				inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;  //평준화.

				JoyStick_img.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (Bg_img.rectTransform.sizeDelta.x / 3),
					inputVector.z * (Bg_img.rectTransform.sizeDelta.y / 3));
				//joystick_img의 위치를 bg_img의 사이즈/3 의 길이로 이동한다. 
		
		}
	}
	public virtual void OnPointerDown(PointerEventData ped)//터치 시작 좌표를 넘긴다.
	{
		OnDrag (ped);
	}
	public virtual void OnPointerUp(PointerEventData ped) //터치 중지
	{
		inputVector = Vector3.zero; //안누르니 초기화
		JoyStick_img.rectTransform.anchoredPosition=Vector3.zero; //땠을때 joystick_img 위치 중앙으로 초기화.
	}

//	private bool IsPointerOverUIObject(PointerEventData ped)
//	{
//
//		ped.position
//		= new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
//
//		List<RaycastResult> results = new List<RaycastResult>();
//		EventSystem.current.RaycastAll(ped, results);
//		return results.Count > 0;
//	}

}
