using UnityEngine;
using System.Collections;

public class PictureLib_Scale : MonoBehaviour
{
    public GameObject Wall;
    void OnMouseDown()
    {
        transform.parent.transform.Find("Zoom").gameObject.SetActive(true);
        var s = Wall.GetComponent<PictureLib>();
        var pn = System.Int32.Parse(transform.Find("txt").GetComponent<TextMesh>().text);

        transform.parent.transform.Find("Zoom/ZoomPicNum").GetComponent<TextMesh>().text = "Рисунок " + pn;

		s.loadPicture(transform.parent.transform.Find("Zoom/ZoomPicture").gameObject, s.textures[pn - 1], 0.9f, 0.4f);
    }
}
