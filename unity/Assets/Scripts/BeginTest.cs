using UnityEngine;

public class BeginTest : MonoBehaviour
{

    GameObject BoardStatic, BoardToMove, Cube1, Cube2, Cube3, Cube4, Cube5;
    private Board scr;

    void OnMouseDown()
    {
        BoardStatic.transform.localPosition.z = 17.95; //выдвинуть на вид заголовок вопроса и кнопки переключения
        BoardToMove.transform.Find("BoardGroup").animation.Play("BoardAppear"); //запустить вылет стенда

        this.transform.localPosition.z = 0.5;
        transform.parent.transform.Find("Result").localPosition.z = 0.5;
        transform.parent.transform.Find("Minimum").localPosition.z = 0.5;
        transform.parent.transform.Find("Conclusion").localPosition.z = 0.5;

        scr = BoardToMove.transform.Find("BoardGroup").GetComponent<Board>();
        scr.i = 0; scr.initializeArrays(); scr.UpdateBeginning();

        //выдвинуть нужные кубы
        Cube1.animation.Play("CubeInactiveUp");
        Cube2.animation.Play("CubeInactiveUp");
        Cube3.animation.Play("CubeInactiveUp");
        if (scr.qAnsNum[0] > 3) Cube4.animation.Play("CubeInactiveUp");
        if (scr.qAnsNum[0] > 4) Cube5.animation.Play("CubeInactiveUp");
    }
}
