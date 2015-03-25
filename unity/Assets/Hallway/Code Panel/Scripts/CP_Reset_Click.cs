using UnityEngine;

public class CP_Reset_Click : MonoBehaviour
{
    void OnMouseDown()
    {
        if (this.enabled)
        {
            var txt = transform.parent.transform.Find("Field Group").transform.Find("Field Text");
            var l = txt.GetComponent<TextMesh>().text.Length;
            if (l <= 0)
            {
                transform.Find("G Reset").animation.Play("CP ButtonError");
            }
            else
            {
                transform.Find("G Reset").animation.Play("CP ButtonLight");
                //это мы так удаляем последний символ
                //напрашивающийся slice работать не захотел
                var s = "";
                for (var i = 0; i < l - 1; i++)
                    s = s + txt.GetComponent<TextMesh>().text[i];
                txt.GetComponent<TextMesh>().text = s;
            }
        }
    }
}
