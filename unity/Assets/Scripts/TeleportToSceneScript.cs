using UnityEngine;

public class TeleportToSceneScript : MonoBehaviour
{
    public string SceneNameRoom;
    public DataStructures.ThemeContent content;
    public int theme_num;
    public int content_num;

    void OnTriggerEnter()
    {
        Global.content = content;
        Global.theme_num = theme_num;
        Global.content_num = content_num;
        PhotonNetwork.LoadLevel(SceneNameRoom);
    }
}