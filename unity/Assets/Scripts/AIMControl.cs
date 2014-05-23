using UnityEngine;
using System.Collections;

public class AIMControl : MonoBehaviour {

    private Animator anim;							// a reference to the animator on the character
    AnimatorStateInfo state;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        state = anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Finish"))
        {
            GameObject.Find("Robot").transform.position = GameObject.Find("RobotSpawnPlace").transform.position;
            GameObject.Find("Robot").transform.rotation = GameObject.Find("RobotSpawnPlace").transform.rotation;
            anim.SetBool("Excursion", false);
        }
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Excursion", true);
        }        
    }
}
