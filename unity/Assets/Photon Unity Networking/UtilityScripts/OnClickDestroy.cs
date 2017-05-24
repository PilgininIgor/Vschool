using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class OnClickDestroy : Photon.MonoBehaviour
{
    public bool DestroyByRpc;

    public bool connect;

    [RPC]
    public void DestroyRpc()
    {
        GameObject.Destroy(this.gameObject);
        PhotonNetwork.UnAllocateViewID(this.photonView.viewID);
    }

    void OnClick()
    {
        if (!DestroyByRpc)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else
        {
            this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
        }
    }
}
