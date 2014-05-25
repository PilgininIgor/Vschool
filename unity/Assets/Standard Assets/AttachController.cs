using UnityEngine;
using System.Collections;


//Role of this controller is assigning dynamically loaded avatar to joysticks and cameras.
public class AttachController : MonoBehaviour
{
	public GameObject avatar;
	public Component cameraChange;
	public GameObject cameraScript;
	// Use this for initialization
	void Start ()
	{
		if (GameObject.Find ("Lerpz"))
			avatar = GameObject.Find ("Lerpz");
		if (GameObject.Find ("Worker"))
			avatar = GameObject.Find ("Worker");
		if (GameObject.Find ("AngryBot"))
			avatar = GameObject.Find ("AngryBot");
		if (GameObject.Find ("OldMan"))
			avatar = GameObject.Find ("OldMan");
		if (GameObject.Find ("Man"))
			avatar = GameObject.Find ("Man");
		if (GameObject.Find ("Woman"))
			avatar = GameObject.Find ("Woman");
		if (GameObject.Find ("Girl"))
			avatar = GameObject.Find ("Girl");
		if (GameObject.Find ("Boy"))
			avatar = GameObject.Find ("Boy");
		if (avatar != null) 
		{
			avatar.transform.position = new Vector3 (-31.3F, 1F, -29.7F);
			avatar.transform.Rotate (0, 90, 0);
			var tpc = avatar.GetComponent<ThirdPersonController>();
			tpc.enabled = true;
			
			var oc = cameraScript.GetComponent<OrbitCam>();
			oc.target = avatar.transform;
			
			var jb = GameObject.Find("Dual Joysticks").GetComponent<JoysticksBehaviour>();
			jb.targetCamera = oc;
			jb.tpc = tpc;
		}
	}
}