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
                p1.transform.localPosition.y = 0.2;
                p1.GetComponent<TextMesh>().text = p[0].header; p1.AddComponent<BoxCollider>();
                p1.GetComponent<BoxCollider>().size.z = 0.2; p1.GetComponent<BoxCollider>().center.y = -0.15;
                break;
            case 2:
                p1.active = true; p2.active = true; p3.active = false; p4.active = false;
                p1.transform.localPosition.y = 0.5; p2.transform.localPosition.y = -0.2;
                p1.GetComponent<TextMesh>().text = p[0].header; p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header; p2.AddComponent<BoxCollider>();
                p1.GetComponent<BoxCollider>().size.z = 0.2; p1.GetComponent<BoxCollider>().center.y = -0.15;
                p2.GetComponent<BoxCollider>().size.z = 0.2; p2.GetComponent<BoxCollider>().center.y = -0.15;
                break;
            case 3:
                p1.active = true; p2.active = true; p3.active = true; p4.active = false;
                p1.transform.localPosition.y = 0.9; p2.transform.localPosition.y = 0.2;
                p3.transform.localPosition.y = -0.5;
                p1.GetComponent<TextMesh>().text = p[0].header; p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header; p2.AddComponent<BoxCollider>();
                p3.GetComponent<TextMesh>().text = p[2].header; p3.AddComponent<BoxCollider>();
                p1.GetComponent<BoxCollider>().size.z = 0.2; p1.GetComponent<BoxCollider>().center.y = -0.15;
                p2.GetComponent<BoxCollider>().size.z = 0.2; p2.GetComponent<BoxCollider>().center.y = -0.15;
                p3.GetComponent<BoxCollider>().size.z = 0.2; p3.GetComponent<BoxCollider>().center.y = -0.15;
                break;
            case 4:
                p1.active = true; p2.active = true; p3.active = true; p4.active = true;
                p1.transform.localPosition.y = 1.2; p2.transform.localPosition.y = 0.5;
                p3.transform.localPosition.y = -0.2; p4.transform.localPosition.y = -0.9;
                p1.GetComponent<TextMesh>().text = p[0].header; p1.AddComponent<BoxCollider>();
                p2.GetComponent<TextMesh>().text = p[1].header; p2.AddComponent<BoxCollider>();
                p3.GetComponent<TextMesh>().text = p[2].header; p3.AddComponent<BoxCollider>();
                p4.GetComponent<TextMesh>().text = p[3].header; p4.AddComponent<BoxCollider>();
                p1.GetComponent<BoxCollider>().size.z = 0.2; p1.GetComponent<BoxCollider>().center.y = -0.15;
                p2.GetComponent<BoxCollider>().size.z = 0.2; p2.GetComponent<BoxCollider>().center.y = -0.15;
                p3.GetComponent<BoxCollider>().size.z = 0.2; p3.GetComponent<BoxCollider>().center.y = -0.15;
                p4.GetComponent<BoxCollider>().size.z = 0.2; p4.GetComponent<BoxCollider>().center.y = -0.15;
                break;
        }
    }
}
