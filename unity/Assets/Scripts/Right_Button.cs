using UnityEngine;

public class Right_Button : MonoBehaviour
{
    public bool pic_enlarged = false;
    public bool animation_in_progress = false;
    public Texture2D pic;

    void AnimationStart() { animation_in_progress = true; }
    void AnimationStop() { animation_in_progress = false; }

    void LoadPicture()
    {
        Board scr = transform.parent.GetComponent<Board>();
		HttpConnector httpConnector = GameObject.Find ("Bootstrap").GetComponent<HttpConnector> ();
		Debug.Log (scr.qPicPath[scr.i]);
		httpConnector.Get (scr.qPicPath[scr.i],
			www => {
			GetComponent<Renderer>().material.mainTexture = www.texture;
		},
			www => {

			});
    }

    void UnloadPicture()
    {
		GetComponent<Renderer>().material.mainTexture = pic;
    }

    void OnMouseDown()
    {
        if ((!animation_in_progress) && (GetComponent<Renderer>().enabled))
        {
            if (!pic_enlarged)
            {
				Debug.Log ("start load");
                pic_enlarged = true;
				LoadPicture ();
                GetComponent<Animation>().Play("PictureAnimUp");
            }
            else
            {
				Debug.Log ("start unload");
                pic_enlarged = false;
				UnloadPicture ();
                GetComponent<Animation>().Play("PictureAnimDown");
            }
        }
    }
}
