using UnityEngine;

public class CubeScript : MonoBehaviour
{
    int i;
    public bool is_active = false;
    public bool animation_in_progress = false;

    void AnimationStart() { animation_in_progress = true; }
    void AnimationStop() { animation_in_progress = false; }

    void SetAnswer(bool ans)
    {
        var scr = GameObject.Find("BoardGroup").GetComponent<Board>();
        if (name == "Cube1") scr.a[scr.i][0] = ans;
        else if (name == "Cube2") scr.a[scr.i][1] = ans;
        else if (name == "Cube3") scr.a[scr.i][2] = ans;
        else if (name == "Cube4") scr.a[scr.i][3] = ans;
        else if (name == "Cube5") scr.a[scr.i][4] = ans;
    }

    void OnMouseDown()
    {
        if (!animation_in_progress)
        {
            if (!is_active)
            {
                i = Random.Range(1, 5);
                switch (i)
                {
                    case 1:
                        GetComponent<Animation>().Play("CubeAnim1");
                        break;
                    case 2:
                        GetComponent<Animation>().Play("CubeAnim2");
                        break;
                    case 3:
                        GetComponent<Animation>().Play("CubeAnim3");
                        break;
                    case 4:
                        GetComponent<Animation>().Play("CubeAnim4");
                        break;
                }
                is_active = true;
                SetAnswer(true);
            }
            else
            {
                GetComponent<Animation>().Play("CubeReturn");
                is_active = false;
                SetAnswer(false);
            }
        }
    }
}
