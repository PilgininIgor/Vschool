using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Collections.Generic;

public class NetworkCharacterAnimSync : Photon.MonoBehaviour 
{

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;
    Thread thread;

    Animator anim;

    //ThirdPersonCamera cameraScript;
    BotControlScript controllerScript;

    void Awake()
    {
        //cameraScript = GetComponent<ThirdPersonCamera>();
        controllerScript = GetComponent<BotControlScript>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            //cameraScript.enabled = true;
            controllerScript.enabled = true;
        }
        else
        {
            //cameraScript.enabled = false;
            controllerScript.enabled = false;
        }

        //gameObject.name = gameObject.name + photonView.viewID;
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("IVPE, Add the animator to this prefab");
        else
            Debug.Log("Animator is here!");
	}
	
	// Update is called once per frame
	void Update () {
       
        if (!photonView.isMine)
        {
            //transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
            transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime*5);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime*5);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(anim.GetFloat("Speed"));
            //stream.SendNext(anim.GetFloat("Direction"));
            //stream.SendNext(anim.GetBool("Jump"));
            //stream.SendNext(anim.GetFloat("ColliderHeight"));
            //stream.SendNext(anim.GetBool("JumpDown"));
            //stream.SendNext(anim.GetFloat("ColliderY"));
            //stream.SendNext(anim.GetBool("Wave"));
            //stream.SendNext(anim.GetBool("Dying"));
            //stream.SendNext(anim.GetBool("JumpUp"));
            //stream.SendNext(anim.GetBool("Dancing"));
            //stream.SendNext(anim.GetBool("Slide"));
        }
        else 
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            //anim.SetFloat("Speed", (float)stream.ReceiveNext());
            //anim.SetFloat("Direction", (float)stream.ReceiveNext());
            //anim.SetBool("Jump", (bool)stream.ReceiveNext());
            //anim.SetFloat("ColliderHeight", (float)stream.ReceiveNext());
            //anim.SetBool("JumpDown", (bool)stream.ReceiveNext());
            //anim.SetFloat("ColliderY", (float)stream.ReceiveNext());
            //anim.SetBool("Wave", (bool)stream.ReceiveNext());
            //anim.SetBool("Dying", (bool)stream.ReceiveNext());
            //anim.SetBool("JumpUp", (bool)stream.ReceiveNext());
            //anim.SetBool("Dancing", (bool)stream.ReceiveNext());
            //anim.SetBool("Slide", (bool)stream.ReceiveNext());
        }
    }
}
