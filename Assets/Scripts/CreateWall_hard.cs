using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall_hard : MonoBehaviour {

	public GameObject Wall_Prefab;
	public GameObject Floor_prefab;
	public GameObject WallContainer;
	public Transform Player_Position;

	public int row_count;
	public int col_count;
	public int Current_Cell;
	public List<int> cell_stack;
	private int count = 0;
	private bool[,] check;
	private int stack_count=0;

	void Start ()
	{
		FloorMake (col_count,row_count);

		cell_stack = new List<int> ();   //셀이 지나간곳은 스택으로 저장 , 이유는 셀에서 더이상 갈곳이없을때 스택에 저장되어있는 순으로 돌아가서 다시 갈곳이있는 셀로이동하기위해서이다. 
		cell_stack.Clear();
		WallCreate (row_count, col_count); //정사각형의 벽 생성

		check = new bool[col_count,row_count];//지나간 셀인지 체크 
		for (int i = 0; i < col_count; i++) {
			for (int j = 0; j < row_count; j++) {
				check [i, j] = false;
			}
		}
		
		//Current_Cell =	Random.Range (0, col_count * row_count);	//0=< n<col_count*row_count
		MazeCreate(Current_Cell);
	}
	private void FloorMake(int col_count,int row_count)
	{
		float wall_sizeX = Wall_Prefab.transform.localScale.x;
		float wall_sizeY = Wall_Prefab.transform.localScale.y;
		float wall_sizeZ = Wall_Prefab.transform.localScale.z;

		GameObject Floor = Instantiate (Floor_prefab, new Vector3 (wall_sizeX * row_count/2, 0, -wall_sizeX * col_count/2), Quaternion.identity);
		Floor.transform.localScale= new Vector3(wall_sizeX*row_count/10 + 10.0f,1,wall_sizeX*col_count/10  +10.0f);  //뒤에 10.0f는 바닥 조금 더 크게 하기위해서 
	}
	private void WallCreate(int row_count,int col_count)
	{
		float wall_sizeX = Wall_Prefab.transform.localScale.x;
		float wall_sizeY = Wall_Prefab.transform.localScale.y;
		float wall_sizeZ = Wall_Prefab.transform.localScale.z;

		for (int i = 0; i < col_count*2+1 ; i++) 
		{
			for (int j = 0; j < row_count+1; j++) 
			{
				//미로 생성	
				if (i % 2 == 0) 
				{
					if (j != row_count) {
						GameObject container = Instantiate (Wall_Prefab, new Vector3 (+wall_sizeX / 2 + j * wall_sizeX, wall_sizeY / 2, (-1) * i * wall_sizeX / 2), Quaternion.identity);
						container.transform.SetParent (WallContainer.transform, true);
						container.name = count.ToString ();
						count++;
					}
				} 

				else
				{
					GameObject container = Instantiate (Wall_Prefab, new Vector3 (wall_sizeX*j,wall_sizeY/2,(-1)*wall_sizeX/2*i), Quaternion.Euler (0, -90, 0));
					container.transform.SetParent (WallContainer.transform, true);
					container.name = count.ToString ();
					count++;
				}
			}
		}
	}
	private void MazeCreate(int Current_Cell)
	{
		int[] neibor_count = new int[4];
		int length = 0;
		int neibor_up = Current_Cell - row_count;
		int neibor_left = Current_Cell - 1;
		int neibor_down = Current_Cell + row_count;
		int neibor_right = Current_Cell + 1;
		int next_cell;
		int destroy_wall=-1;
		int check_count = 0;

		//최근 방문한 셀을 리스트에 저장 
		cell_stack.Add(Current_Cell);
		stack_count++;

		check [Current_Cell / row_count, Current_Cell % row_count] = true;

		//왼쪽이 있다면 && 왼쪽이 방문한곳이 아니라면 
		if (Current_Cell % row_count != 0&&check[neibor_left/row_count,neibor_left%row_count]==false) {
			neibor_count [length] = Current_Cell - 1;
			length++;
		}
		//밑이 있다면 && 밑이 방문한곳이 아니라면 
		if (Current_Cell / row_count != col_count-1 &&check[neibor_down/row_count,neibor_down%row_count]==false) {
			neibor_count [length] = Current_Cell +row_count;
			length++;
		}
		//오른쪽이 있다면 && 오른쪽이 방문한곳이 아니라면 
		if ((Current_Cell+1)% row_count != 0 &&check[neibor_right/row_count,neibor_right%row_count]==false) {
			neibor_count [length] = Current_Cell +1;
			length++;
		}
		//위가 있다면  &	&위가 방문한곳이 아니라면 
		if (Current_Cell / row_count != 0 &&check[neibor_up/row_count,neibor_up%row_count] ==false) {
			neibor_count [length] = Current_Cell - row_count;
			length++;
		}

		if (length > 0) {
			int next_random = Random.Range (0, length);
			next_cell = neibor_count [next_random];


			if (next_cell + row_count == Current_Cell) { //선택된 다음셀이 전셀의 위라면
				destroy_wall = (2*row_count+1)*(Current_Cell/row_count)+(Current_Cell%row_count);
			} else if (next_cell + 1 == Current_Cell) {  //선택된 다음셀이 전셀의 왼쪽이라면 
				destroy_wall =(2*row_count+1)*(Current_Cell/row_count)+(Current_Cell%row_count)+row_count;
			} else if (next_cell - row_count == Current_Cell) { //선택된 다음셀이 전셀의 밑이라면
				destroy_wall = (2*row_count+1)*(Current_Cell/row_count)+(Current_Cell%row_count)+(2*row_count+1);
			} else if (next_cell - 1 == Current_Cell) {  //선택된 다음셀이 전셀의 오른쪽이라면
				destroy_wall =(2*row_count+1)*(Current_Cell/row_count)+(Current_Cell%row_count)+row_count+1;
			}
		//	Debug.Log (Current_Cell+" "+next_cell+" "+length+" "+next_random+" "+destroy_wall.ToString());

			//DestroyObject (GameObject.Find (destroy_wall.ToString()));
			GameObject.Find(destroy_wall.ToString()).transform.GetComponent<BoxCollider>().isTrigger = true;
		//	GameObject.Find (destroy_wall.ToString ()).transform.GetComponent<Transform> ().localScale = new Vector3 (10, 10, 10);
		}
		else { //갈곳이 없다면 
			
			cell_stack.RemoveAt (cell_stack.Count-1);
			next_cell = cell_stack[cell_stack.Count-1];
			cell_stack.RemoveAt (cell_stack.Count-1);
		}

//		for(int i=0;i<length;i++)
//			Debug.Log (neibor_count[i]);

		for (int i = 0; i < col_count; i++) {
			for (int j = 0; j < row_count; j++) {
				if (check [i, j] == false) 
					check_count++;
			}
		}


			
		
		if (check_count >0) {
			MazeCreate (next_cell);
		} else
			return;
	}
//	private void WallDestroy(int n,int count,int row_count,int col_count)
//	{
//		int random;
//		int wall_count=0;  //인접한 벽의 name을 지우기위함.
//		int next_wall=0;	//인접한 지역으로 넘어갈수 다음 n
//
//		if (count != 1) {
//			//가장자리 (인접면이 2개)
//			if ((n == 0) || (n == col_count - 1) || (n == col_count * (row_count - 1)) || (n == col_count * row_count - 1)) {
//				random = Random.Range (0, 2);
//
//				if (n == 0) {			//왼쪽위
//					switch (random) {
//					case 0://오
//						wall_count = n + col_count + 1;
//						next_wall = n + 1;
//						break;
//					case 1://밑
//						wall_count = n + (col_count * 2) + 1;
//						next_wall = n + col_count;	
//						break;
//					}
//				} else if (n == col_count - 1){ 		//오른쪽위
//					switch (random) {
//					case 0: //왼
//						wall_count = n * 2 + 1;
//						next_wall = n - 1;
//						break;
//					case 1://밑
//						wall_count = n + (col_count * 2) + 1;
//						next_wall = n * 2 + 1;
//						break;
//					}
//				} else if (n == col_count * (row_count - 1)) { //왼쪽 아래
//					switch (random) {
//					case 0: //위
//						wall_count = n * 2 + col_count - 1;
//						next_wall = n - col_count;
//						break;
//					case 1://오
//						wall_count = (n * 2) + (col_count * 2);
//						next_wall = n + 1;
//						break;
//					}
//				} else if (n == col_count * row_count - 1) //오른쪽 아래
//				{
//					switch (random) {
//					case 0: //왼
//						wall_count = n * 2 + col_count;
//						next_wall = n - 1;
//						break;
//					case 1: //위
//						wall_count = n * 2;
//						next_wall = n - col_count;
//						break;
//					}
//				}
//				//	Debug .Log("난수 : " + n + "가장자리 ");
//			}
//		//가장자리를 제외한 4변  (인접면이 3개)
//		else if ((n > 0 && n < col_count - 1) || (n % col_count == 0) || ((n + 1) % col_count == 0) || ((n > col_count * (row_count - 1)) && (n < col_count * row_count - 1))) {
//				
//
//					random = Random.Range (0, 3);
//
//					if (n > 0 && n < col_count - 1) { //윗변
//						switch (random) { 
//						case 0: //왼
//							wall_count = n + col_count;
//							next_wall = n - 1;
//							break;
//						case 1: //밑
//							wall_count = n + (col_count * 2) + 1;
//							next_wall = n + col_count;
//							break;
//						case 2: //오
//							wall_count = n + col_count + 1;
//							next_wall = n + 1;
//							break;
//						}
//					} else if (n % col_count == 0) { //왼쪽 변
//						switch (random) {
//						case 0://위
//							wall_count = (n / col_count) + (n * 2);
//							next_wall = n - col_count;
//							break;
//						case 1://오
//							wall_count = (n / col_count) + (n * 2) + col_count + 1;
//							next_wall = n + 1;
//							break;
//						case 2://밑
//							wall_count = (n + col_count) / col_count + (n + col_count) * 2;
//							next_wall = n + col_count;
//							break;
//						}
//					} else if ((n + 1) % col_count == 0) {// 오른쪽 변
//						switch (random) {
//						case 0://위
//							wall_count = (n / col_count) + ((n - col_count + 1) * 2) + col_count - 1;
//							next_wall = n - col_count;
//							break;
//						case 1://왼
//							wall_count = n * 2 + n / col_count + 1;
//							next_wall = n - 1;
//							break;
//						case 2://밑
//							wall_count = ((n + col_count) / col_count) + (n + 1) * 2 + col_count - 1;
//							next_wall = n + col_count;
//							break;
//						}
//					} else if ((n > col_count * (row_count - 1)) && (n < col_count * row_count - 1)) {//밑 변
//						switch (random) {
//						case 0://왼
//							wall_count = (n % col_count) + (col_count * row_count * 2) - 1;
//							next_wall = n - 1;
//							break;
//						case 1://위
//							wall_count = n * 2 + (col_count - (n % col_count) - 1);
//							next_wall = n - col_count;
//							break;
//						case 2://오
//							wall_count = (n % col_count) + (col_count * row_count * 2);
//							next_wall = n + 1;
//							break;
//						}
//					}
//				
//				//	Debug .Log("난수 : " + n + "네개의변 ");
//			}
//		//그이외에 안에 있는것 (인접면이 4개)
//		else {
//				
//					random = Random.Range (0, 4);
//
//					switch (random) {
//					case 0: //위
//						wall_count = n + (col_count + 1) * (n / col_count);
//						next_wall = n - col_count;
//						break;
//					case 1://오
//						wall_count = n + (col_count + 1) * (n / col_count + 1);
//						next_wall = n + 1;
//						break;
//					case 2://밑
//						wall_count = n + (col_count * 2 + 1) + n / col_count * (col_count + 1);
//						next_wall = n + col_count;
//						break;
//					case 3://왼
//						wall_count = n + (col_count + 1) * (n / col_count + 1) - 1;
//						next_wall = n - 1;
//						break;
//					}
//				
//				//	Debug .Log("난수 : " + n + "나머지");
//			}
//
//			//벽 삭제 코드
//			string Wall_name =wall_count.ToString();
//			Destroy (GameObject.Find (Wall_name));
//			//Debug.Log (Wall_name);
//			//delete_count[n/col_count,n%col_count]=delete_count[n/col_count,n%col_count] - 1;
//			//delete_count [next_wall / col_count,next_wall % col_count] =delete_count [next_wall / col_count,next_wall % col_count]- 1;
//			//재귀 호출 코드
//			WallDestroy(next_wall,count-1,row_count,col_count);
//		}
//		else 
//		{
//			return;
//		}
//	}
}
