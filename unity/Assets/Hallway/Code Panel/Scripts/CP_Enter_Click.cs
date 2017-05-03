using UnityEngine;
using System.Collections;

public class CP_Enter_Click : MonoBehaviour
{

    string correct_code = "5678";

    void SetScriptsCondition(bool cond)
    {
        transform.parent.transform.Find("B0 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B1 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B2 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B3 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B4 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B5 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B6 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B7 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B8 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B9 Group").GetComponent<CP_Digit_Click>().enabled = cond;
        transform.parent.transform.Find("B Reset Group").GetComponent<CP_Reset_Click>().enabled = cond;
    }

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            var txt = transform.parent.transform.Find("Field Group").transform.Find("Field Text");
            var ind = transform.parent.transform.Find("Indicator");
            var l = txt.GetComponent<TextMesh>().text.Length;
            if (l == 0)
            {
                //если пользователь не ввел вообще ничего, сигнализируем об ошибке самой кнопкой
                transform.Find("G Enter").GetComponent<Animation>().Play("CP ButtonError");
            }
            else
            {
                //если пользователь ввел хоть что-то, проверяем это на соответствие правильному коду
                //запускаем стандартную анимацию нажатой кнопки
                transform.Find("G Enter").GetComponent<Animation>().Play("CP ButtonLight");
                //отрубаем скрипты всех остальных кнопок, чтобы пользователь не мешал процессу
                SetScriptsCondition(false);
                if (txt.GetComponent<TextMesh>().text == correct_code)
                {
                    //выдерживаем небольшую паузу
                    StartCoroutine(Wait(0.4f));
                    //окрашиваем индикатор в зеленый, показывая, что код верный
                    ind.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.235f, 0.235f, 0.235f));
                    ind.GetComponent<Renderer>().material.SetColor("_SpecColor", new Color(0, 1, 0));
                    //снова задержка, чтобы пользователь успел заметить
                    StartCoroutine(Wait(1.5f));
                    //сигнализируем общему скрипту о том, что пора закругляться
                    transform.parent.GetComponent<CP_Begin_Click>().Finish();
                }
                else
                {
                    //выдерживаем небольшую паузу
                    //yield WaitForSeconds(0.4);
                    //окрашиваем индикатор в красный, показывая, что код неверный
                    ind.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.235f, 0.235f, 0.235f));
                    ind.GetComponent<Renderer>().material.SetColor("_SpecColor", new Color(1, 0, 0));
                    //снова задержка, чтобы пользователь успел заметить
                    StartCoroutine(Wait(1.5f));
                    //стираем строку и возвращаем индикатору исходный цвет
                    txt.GetComponent<TextMesh>().text = "";
                    ind.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0));
                    ind.GetComponent<Renderer>().material.SetColor("_SpecColor", new Color(0.235f, 0.235f, 0.235f));
                }
                //возвращаем скрипты во включенное состояние
                SetScriptsCondition(true);
            }
        }
    }
}
