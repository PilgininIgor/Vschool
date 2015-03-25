using UnityEngine;
using System.Collections;

public class LeftArrowScript : MonoBehaviour {

    void OnMouseDown()
    {

        var scr1 = GameObject.Find("Cube1").GetComponent<CubeScript>();
        var scr2 = GameObject.Find("Cube2").GetComponent<CubeScript>();
        var scr3 = GameObject.Find("Cube3").GetComponent<CubeScript>();
        var scr4 = GameObject.Find("Cube4").GetComponent<CubeScript>();
        var scr5 = GameObject.Find("Cube5").GetComponent<CubeScript>();
        var scrb = GameObject.Find("Plane_RightButton").GetComponent<RightButtonScript>();

        if ((!scr1.animation_in_progress) && (!scr2.animation_in_progress)
        && (!scr3.animation_in_progress) && (!scr4.animation_in_progress)
        && (!scr5.animation_in_progress) && (!scrb.animation_in_progress))
        {

            var scr = GameObject.Find("BoardGroup").GetComponent<Board>();
            var anim = GameObject.Find("BoardGroup").GetComponent<Animation>();
            var anim1 = GameObject.Find("Cube1").GetComponent<Animation>();
            var anim2 = GameObject.Find("Cube2").GetComponent<Animation>();
            var anim3 = GameObject.Find("Cube3").GetComponent<Animation>();
            var anim4 = GameObject.Find("Cube4").GetComponent<Animation>();
            var anim5 = GameObject.Find("Cube5").GetComponent<Animation>();

            scr.i = scr.i - 1;
            if (scr.i < 0) scr.i = scr.qText.Length - 1;
            anim.Play("BoardAnim");

            if (scrb.pic_enlarged)
            {
                var animb = GameObject.Find("Plane_RightButton").GetComponent<Animation>();
                animb.Play("PictureAnimDown");
                scrb.pic_enlarged = false;
            }

            anim1.Play(scr1.is_active ? "CubeActiveDown" : "CubeInactiveDown");
            anim2.Play(scr2.is_active ? "CubeActiveDown" : "CubeInactiveDown");
            anim3.Play(scr3.is_active ? "CubeActiveDown" : "CubeInactiveDown");
            anim4.Play(scr4.is_active ? "CubeActiveDown" : "CubeInactiveDown");
            anim5.Play(scr5.is_active ? "CubeActiveDown" : "CubeInactiveDown");
        }

    }
}
