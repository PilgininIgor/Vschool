using UnityEngine;
using System.Collections;

public class Inst_cube : MonoBehaviour {

	GameObject[] cubes = new GameObject[15];
	int i = 0;
	public GameObject cube0;
	public GameObject cube1;
	public GameObject cube2;
	public GameObject cube3;
	public GameObject cube4;
	public GameObject cube5;
	public GameObject cube6;
	public GameObject cube7;
	public GameObject cube8;
	public GameObject cube9;
	public GameObject cubeA;
	public GameObject cubeB;
	public GameObject cubeC;
	public GameObject cubeD;
	public GameObject cubeE;
	public GameObject cubeF;
	
	public void GenerateCube (string digit) 
	{
		GameObject ob = new GameObject();
		if (digit == "0") 
		{
			ob = Instantiate (cube0, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "1") 
		{
			ob = Instantiate (cube1, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "2") 
		{
			ob = Instantiate (cube2, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "3") 
		{
			ob = Instantiate (cube3, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "4") 
		{
			ob = Instantiate (cube4, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "5") 
		{
			ob = Instantiate (cube5, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "6") 
		{
			ob = Instantiate (cube6, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "7") 
		{
			ob = Instantiate (cube7, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "8") 
		{
			ob = Instantiate (cube8, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "9") 
		{
			ob = Instantiate (cube9, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "A") 
		{
			ob = Instantiate (cubeA, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "B") 
		{
			ob = Instantiate (cubeB, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "C") 
		{
			ob = Instantiate (cubeC, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "D") 
		{
			ob = Instantiate (cubeD, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "E") 
		{
			ob = Instantiate (cubeE, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		if (digit == "F") 
		{
			ob = Instantiate (cubeF, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		Destroy (cubes [i]);
		cubes[i] = ob;
		i++;
		if (i == 15)
		{
			i = 0;
		}
	}

}