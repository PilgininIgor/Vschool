using UnityEngine;
using System.Collections;

public class RightButtonScript : MonoBehaviour
{
    public bool pic_enlarged;
    public bool animation_in_progress;

    void AnimationStart() { animation_in_progress = true; }
    void AnimationStop() { animation_in_progress = false; }

    void LoadPicture()
    {
        var scr = GameObject.Find("BoardGroup").GetComponent<Board>();
        var www = new WWW(scr.qPicPath[scr.i]);
        GameObject.Find("Plane_RightButton").GetComponent<Renderer>().material.mainTexture = www.texture;
    }

    void UnloadPicture()
    {
        GameObject.Find("Plane_RightButton").GetComponent<Renderer>().material.mainTexture = (Texture) Resources.Load("Picture");
    }

    void OnMouseDown()
    {
        if ((animation_in_progress) || (!GetComponent<Renderer>().enabled)) return;
        if (pic_enlarged)
        {
            pic_enlarged = false;
            GetComponent<Animation>().Play("PictureAnimDown");
        }
        else
        {
            pic_enlarged = true;
            GetComponent<Animation>().Play("PictureAnimUp");
        }
    }
}
