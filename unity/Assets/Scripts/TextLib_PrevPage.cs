using UnityEngine;

public class TextLib_PrevPage : MonoBehaviour
{
    public GameObject background;
    void OnMouseDown()
    {
        var scr = background.GetComponent<TextLib>();
        scr.current_page = scr.current_page - 1;
        if (scr.current_page < 1) scr.current_page = scr.pages_number;
        scr.t.GetComponent<TextMesh>().text = scr.p[scr.current_page - 1];

        scr.c.GetComponent<TextMesh>().text = "Стр." + scr.current_page + " из " + scr.pages_number;

        scr.CheckIfComplete(); //статистика
    }
}
