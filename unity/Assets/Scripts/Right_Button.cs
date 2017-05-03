using UnityEngine;

public class Right_Button : MonoBehaviour
{
    public bool pic_enlarged = false;
    public bool animation_in_progress = false;
    Texture2D pic;

    void AnimationStart() { animation_in_progress = true; }
    void AnimationStop() { animation_in_progress = false; }

    void LoadPicture()
    {
        var scr = transform.parent.GetComponent<Board>();
        var www = new WWW(scr.qPicPath[scr.i]);
        var Plane_RightButton = transform.parent.transform.Find("Plane_RightButton");
        Plane_RightButton.GetComponent<Renderer>().material.mainTexture = www.texture;
    }

    void UnloadPicture()
    {
        var Plane_RightButton = transform.parent.transform.Find("Plane_RightButton");
        Plane_RightButton.GetComponent<Renderer>().material.mainTexture = pic;
    }

    void OnMouseDown()
    {
        if ((!animation_in_progress) && (GetComponent<Renderer>().enabled))
        {
            if (!pic_enlarged)
            {
                pic_enlarged = true;
                GetComponent<Animation>().Play("PictureAnimUp");
            }
            else
            {
                pic_enlarged = false;
                GetComponent<Animation>().Play("PictureAnimDown");
            }
        }
    }
}
