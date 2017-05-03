using UnityEngine;
public class Cube : MonoBehaviour {
    int i;
    public bool is_active;
    public bool animation_in_progress;

    void AnimationStart() { animation_in_progress = true; }
    void AnimationStop() { animation_in_progress = false; }

    void SetAnswer(bool ans)
    {
        var scr = GameObject.Find("BoardGroup").GetComponent<Board>();
        switch (name)
        {
            case "Cube1":
                scr.a[scr.i][0] = ans;
                break;
            case "Cube2":
                scr.a[scr.i][1] = ans;
                break;
            case "Cube3":
                scr.a[scr.i][2] = ans;
                break;
            case "Cube4":
                scr.a[scr.i][3] = ans;
                break;
            case "Cube5":
                scr.a[scr.i][4] = ans;
                break;
        }
    }

    void OnMouseDown() {
	if (!animation_in_progress) {
		if (!is_active) {
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
		} else {
			GetComponent<Animation>().Play("CubeReturn");
			is_active = false;
		}
        SetAnswer(is_active);
	}
}
}
