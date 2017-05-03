using UnityEngine;

public class BeginTest : MonoBehaviour
{

    public GameObject BoardStatic, BoardToMove, Cube1, Cube2, Cube3, Cube4, Cube5;
    private Board scr;

    void OnMouseDown()
    {
        var p = BoardStatic.transform.localPosition;
        p.z = 17.95f;
        BoardStatic.transform.localPosition = p; //выдвинуть на вид заголовок вопроса и кнопки переключения
        BoardToMove.transform.Find("BoardGroup").GetComponent<Animation>().Play("BoardAppear"); //запустить вылет стенда

        p = transform.localPosition;
        p.z = 0.5f;
        transform.localPosition = p;

        p = transform.parent.transform.Find("Result").localPosition;
        p.z = 0.5f;
        transform.parent.transform.Find("Result").localPosition = p;

        p = transform.parent.transform.Find("Minimum").localPosition;
        p.z = 0.5f;
        transform.parent.transform.Find("Minimum").localPosition = p;

        p = transform.parent.transform.Find("Conclusion").localPosition;
        p.z = 0.5f;
        transform.parent.transform.Find("Conclusion").localPosition = p;

        scr = BoardToMove.transform.Find("BoardGroup").GetComponent<Board>();
        scr.i = 0; scr.initializeArrays(); scr.UpdateBeginning();

        //выдвинуть нужные кубы
        Cube1.GetComponent<Animation>().Play("CubeInactiveUp");
        Cube2.GetComponent<Animation>().Play("CubeInactiveUp");
        Cube3.GetComponent<Animation>().Play("CubeInactiveUp");
        if (scr.qAnsNum[0] > 3) Cube4.GetComponent<Animation>().Play("CubeInactiveUp");
        if (scr.qAnsNum[0] > 4) Cube5.GetComponent<Animation>().Play("CubeInactiveUp");
    }
}
