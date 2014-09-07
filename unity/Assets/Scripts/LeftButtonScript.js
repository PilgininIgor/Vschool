var looking_at_question = true;

function OnMouseDown() {
	var scr = GameObject.Find("BoardGroup").GetComponent("BoardScript");
	if (looking_at_question) {
		looking_at_question = false;
		renderer.material = Resources.Load("button_question");
		if (scr.qType[scr.i] == 0) {
			GameObject.Find("Text_Question").GetComponent(TextMesh).text = scr.qAns[scr.i];
		} else {
			GameObject.Find("Text_Question").renderer.enabled = false;
			GameObject.Find("Plane_Pic_Answers").renderer.enabled = true;
			var www : WWW = new WWW (scr.qAns[scr.i]);
			yield www;
			GameObject.Find("Plane_Pic_Answers").renderer.material.mainTexture = www.texture;
			GameObject.Find("Plane_Pic_Answers").renderer.material.mainTextureScale = Vector2(-1, -1);
		}
	} else {
		looking_at_question = true;
		renderer.material = Resources.Load("button_answers");
		GameObject.Find("Text_Question").renderer.enabled = true;
		GameObject.Find("Plane_Pic_Answers").renderer.enabled = false;
		GameObject.Find("Text_Question").GetComponent(TextMesh).text = scr.qText[scr.i];
	}
}