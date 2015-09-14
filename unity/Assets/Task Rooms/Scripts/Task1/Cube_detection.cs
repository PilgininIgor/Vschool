using UnityEngine;
using System.Collections;

public class Cube_detection : MonoBehaviour {

	RaycastHit hit;

	public string GetDigit () {
		if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
		{
			if (hit.collider.gameObject.tag == "DigitCube0")
			{
				return "0";
			}
			if (hit.collider.gameObject.tag == "DigitCube1")
			{
				return "1";
			}
			if (hit.collider.gameObject.tag == "DigitCube2")
			{
				return "2";
			}
			if (hit.collider.gameObject.tag == "DigitCube3")
			{
				return "3";
			}
			if (hit.collider.gameObject.tag == "DigitCube4")
			{
				return "4";
			}
			if (hit.collider.gameObject.tag == "DigitCube5")
			{
				return "5";
			}
			if (hit.collider.gameObject.tag == "DigitCube6")
			{
				return "6";
			}
			if (hit.collider.gameObject.tag == "DigitCube7")
			{
				return "7";
			}
			if (hit.collider.gameObject.tag == "DigitCube8")
			{
				return "8";
			}
			if (hit.collider.gameObject.tag == "DigitCube9")
			{
				return "9";
			}
			if (hit.collider.gameObject.tag == "DigitCubeA")
			{
				return "A";
			}
			if (hit.collider.gameObject.tag == "DigitCubeB")
			{
				return "B";
			}
			if (hit.collider.gameObject.tag == "DigitCubeC")
			{
				return "C";
			}
			if (hit.collider.gameObject.tag == "DigitCubeD")
			{
				return "D";
			}
			if (hit.collider.gameObject.tag == "DigitCubeE")
			{
				return "E";
			}
			if (hit.collider.gameObject.tag == "DigitCubeF")
			{
				return "F";
			}
		}
		return "-";
	}
}
