#pragma strict

var looking_at_question = true;

function OnMouseDown() {
	var scr : Board = transform.parent.GetComponent.<Board>();
	var Text_Question = transform.parent.transform.Find("Text_Question");
	var Plane_Pic_Answers = transform.parent.transform.Find("Plane_Pic_Answers");
	if (looking_at_question) {
		looking_at_question = false;
		transform.Find("Answers").gameObject.active = false;
		transform.Find("Question").gameObject.active = true;
		if (scr.qType[scr.i] == 0) {
			Text_Question.GetComponent(TextMesh).text = scr.qAns[scr.i];
		} else {
			Text_Question.renderer.enabled = false;
			Plane_Pic_Answers.renderer.enabled = true;
			var www : WWW = new WWW (scr.qAns[scr.i]);
			yield www;
			Plane_Pic_Answers.renderer.material.mainTexture = www.texture;
			Plane_Pic_Answers.renderer.material.mainTextureScale = Vector2(-1, -1);
		}
	} else {
		looking_at_question = true;
		transform.Find("Answers").gameObject.active = true;
		transform.Find("Question").gameObject.active = false;
		Text_Question.renderer.enabled = true;
		Plane_Pic_Answers.renderer.enabled = false;
		Text_Question.GetComponent(TextMesh).text = scr.qText[scr.i];
	}
}