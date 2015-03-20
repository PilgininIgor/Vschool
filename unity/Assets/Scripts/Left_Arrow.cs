using UnityEngine;
using System.Collections;

public class Left_Arrow : MonoBehaviour
{

    private GameObject BoardGroup, Cube1, Cube2, Cube3, Cube4, Cube5, Plane_RightButton;

    private void OnMouseDown()
    {

        var scr1 = Cube1.GetComponent<Cube>();
        var scr2 = Cube2.GetComponent<Cube>();
        var scr3 = Cube3.GetComponent<Cube>();
        var scr4 = Cube4.GetComponent<Cube>();
        var scr5 = Cube5.GetComponent<Cube>();
        var scrb = Plane_RightButton.GetComponent<Right_Button>();

        if ((!scr1.animation_in_progress) && (!scr2.animation_in_progress)
            && (!scr3.animation_in_progress) && (!scr4.animation_in_progress)
            && (!scr5.animation_in_progress) && (!scrb.animation_in_progress))
        {

            var scr = BoardGroup.GetComponent<Board>();
            var anim = BoardGroup.GetComponent<Animation>();
            var anim1 = Cube1.GetComponent<Animation>();
            var anim2 = Cube2.GetComponent<Animation>();
            var anim3 = Cube3.GetComponent<Animation>();
            var anim4 = Cube4.GetComponent<Animation>();
            var anim5 = Cube5.GetComponent<Animation>();

            //расчет времени ответа на вопрос
            scr.t[scr.i] += Time.timeSinceLevelLoad - scr.prev_time;
            scr.prev_time = Time.timeSinceLevelLoad;

            scr.i = scr.i - 1;
            if (scr.i < 0) scr.i = scr.qText.Length - 1;
            anim.Play("BoardAnim");

            if (scrb.pic_enlarged)
            {
                var animb = Plane_RightButton.GetComponent<Animation>();
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
