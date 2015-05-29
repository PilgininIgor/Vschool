using UnityEngine;

public class PictureLib_LayoutChange : MonoBehaviour
{
    public GameObject Wall;
    void OnMouseDown()
    {
        var s = Wall.GetComponent<PictureLib>();
        for (var i = 0; i < s.stand.Count; i++)
        {
            Destroy(s.stand[i]);
        }
        s.cl = s.cl + 1;
        if (s.cl >= s.layout_x.Length)
            s.cl = 0;
        s.CreateLayout(s.layout_x[s.cl], s.layout_y[s.cl]);
        s.loadPics(1);
        s.current_page = 1;
    }
}
