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
                Text_Question.GetComponent<Renderer>().enabled = false;
                Plane_Pic_Answers.GetComponent<Renderer>().enabled = true;
				HttpConnector httpConnector = GameObject.Find ("Bootstrap").GetComponent<HttpConnector> ();
				httpConnector.Get (board.qAns[board.i], 
					www => {
						Plane_Pic_Answers.GetComponent<Renderer>().material.mainTexture = www.texture;
						Plane_Pic_Answers.GetComponent<Renderer>().material.mainTextureScale = new Vector2(-1, -1);
					},
					www => {

					});
            }
        }
        else
        {
            looking_at_question = true;
            transform.Find("Answers").gameObject.active = true;
            transform.Find("Question").gameObject.active = false;
            Text_Question.GetComponent<Renderer>().enabled = true;
            Plane_Pic_Answers.GetComponent<Renderer>().enabled = false;
            Text_Question.GetComponent<TextMesh>().text = board.qText[board.i];
        }
    }
}
