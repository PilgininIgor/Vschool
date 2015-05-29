using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour {
	
	public float grabPower = 10.0f;
	public float throwPower = 1.0f;
	public RaycastHit hit;
	public float rayDistance = 4.0f;
	bool grab = false;
	bool _throw = false;
	public Transform offset;
	public Transform mainCamera;
	public GUIText textHints;
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(1)) 
		{
			Physics.Raycast (mainCamera.position, mainCamera.forward, out hit, rayDistance);
			if (hit.rigidbody)
			{
				if (grab != true)
				{
					grab = true;
				}
				else
				{
					grab = false;
					_throw = true;
				}
			}
		}
		if (grab)
		{
			if (hit.rigidbody)
			{
				hit.rigidbody.velocity = (offset.position - (hit.transform.position + hit.rigidbody.centerOfMass))*grabPower;
			}
		}
		if (_throw)
		{
			if (hit.rigidbody)
			{
				hit.rigidbody.velocity = transform.forward * throwPower;
				_throw = false;
			}
		}
	}
	
	public bool IsGrabbed()
	{
		return grab;
	}
}
