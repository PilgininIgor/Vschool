using UnityEngine;

public class Left_Button : MonoBehaviour
{
    public bool looking_at_question = true;
    void OnMouseDown()
    {
        Board board = transform.parent.GetComponent<Board>();
        var Text_Question = transform.parent.transform.Find("Text_Question");
        var Plane_Pic_Answers = transform.parent.transform.Find("Plane_Pic_Answers");
        if (looking_at_question)
        {
            looking_at_question = false;
            transform.Find("Answers").gameObject.active = false;
            transform.Find("Question").gameObject.active = true;
            if (board.qType[board.i] == 0)
            {
                Text_Question.GetComponent<TextMesh>().text = board.qAns[board.i];
            }
            else
            {
                Text_Question.renderer.enabled = false;
                Plane_Pic_Answers.renderer.enabled = true;
                WWW www = new WWW(board.qAns[board.i]);
                Plane_Pic_Answers.renderer.material.mainTexture = www.texture;
                Plane_Pic_Answers.renderer.material.mainTextureScale = new Vector2(-1, -1);
            }
        }
        else
        {
            looking_at_question = true;
            transform.Find("Answers").gameObject.active = true;
            transform.Find("Question").gameObject.active = false;
            Text_Question.renderer.enabled = true;
            Plane_Pic_Answers.renderer.enabled = false;
            Text_Question.GetComponent<TextMesh>().text = board.qText[board.i];
        }
    }
}
