using UnityEngine;

public class ParagraphList : MonoBehaviour
{
    public DataStructures.Paragraph[] p;

    public void Paragraphs(DataStructures.Paragraph[] p)
    {
        var p1 = transform.parent.transform.Find("P1").gameObject;
        var p2 = transform.parent.transform.Find("P2").gameObject;
        var p3 = transform.parent.transform.Find("P3").gameObject;
        var p4 = transform.parent.transform.Find("P4").gameObject;
        //в зависимости от числа параграфов регулируем координаты, а также добавляем к текстам с параграфами
        //Box Collider'ы, чтобы по ним можно было кликать
        switch (p.Length)
        {
            case 1:
                p1.active = true;
                p2.active = p3.active = p4.active = false;
                var t = p1.transform.localPosition;
                t.y = 0.2f;
                p1.transform.localPosition = t;
                p1.GetComponent<TextMesh>().text = p[0].header;
                p1.AddComponent<BoxCollider>();
                t = p1.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p1.GetComponent<BoxCollider>().size = t;
                t = p1.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p1.GetComponent<BoxCollider>().center = t;
                break;
            case 2:
                p1.active = p2.active = true; p3.active = p4.active = false;
                t = p1.transform.localPosition;
                t.y = 0.5f;
                p1.transform.localPosition = t;
                t = p2.transform.localPosition;
                t.y = -0.2f;
                p2.transform.localPosition = t;
                p1.GetComponent<TextMesh>().text = p[0].header; p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header; p2.AddComponent<BoxCollider>();
                t = p1.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p1.GetComponent<BoxCollider>().size = t;
                t = p1.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p1.GetComponent<BoxCollider>().center = t;
                t = p2.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p2.GetComponent<BoxCollider>().size = t;
                t = p2.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p2.GetComponent<BoxCollider>().center = t;
                break;
            case 3:
                p1.active = p2.active = p3.active = true; p4.active = false;
                t = p1.transform.localPosition;
                t.y = 0.9f;
                p1.transform.localPosition = t;
                t = p2.transform.localPosition;
                t.y = 0.2f;
                p2.transform.localPosition = t;
                t = p3.transform.localPosition;
                t.y = -0.5f;
                p3.transform.localPosition = t;
                p1.GetComponent<TextMesh>().text = p[0].header;
                p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header;
                p2.AddComponent<BoxCollider>();
                p3.GetComponent<TextMesh>().text = p[2].header;
                p3.AddComponent<BoxCollider>();
                t = p1.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p1.GetComponent<BoxCollider>().size = t;
                t = p1.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p1.GetComponent<BoxCollider>().center = t;
                t = p2.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p2.GetComponent<BoxCollider>().size = t;
                t = p2.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p2.GetComponent<BoxCollider>().center = t;
                t = p3.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p3.GetComponent<BoxCollider>().size = t;
                t = p3.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p3.GetComponent<BoxCollider>().center = t;
                break;
            case 4:
                p1.active = p2.active = p3.active = p4.active = true;
                t = p1.transform.localPosition;
                t.y = 1.2f;
                p1.transform.localPosition = t;
                t = p2.transform.localPosition;
                t.y = 0.5f;
                p2.transform.localPosition = t;
                t = p3.transform.localPosition;
                t.y = -0.2f;
                p3.transform.localPosition = t;
                t = p4.transform.localPosition;
                t.y = -0.9f;
                p4.transform.localPosition = t;
                p1.GetComponent<TextMesh>().text = p[0].header;
                p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header;
                p2.AddComponent<BoxCollider>();
                p3.GetComponent<TextMesh>().text = p[2].header;
                p3.AddComponent<BoxCollider>();
                p4.GetComponent<TextMesh>().text = p[3].header;
                p4.AddComponent<BoxCollider>();
                t = p1.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p1.GetComponent<BoxCollider>().size = t;
                t = p1.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p1.GetComponent<BoxCollider>().center = t;
                t = p2.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p2.GetComponent<BoxCollider>().size = t;
                t = p2.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p2.GetComponent<BoxCollider>().center = t;
                t = p3.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p3.GetComponent<BoxCollider>().size = t;
                t = p3.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p3.GetComponent<BoxCollider>().center = t;
                t = p4.GetComponent<BoxCollider>().size;
                t.z = 0.2f;
                p4.GetComponent<BoxCollider>().size = t;
                t = p4.GetComponent<BoxCollider>().center;
                t.y = -0.15f;
                p4.GetComponent<BoxCollider>().center = t;
                break;
        }
    }
}
