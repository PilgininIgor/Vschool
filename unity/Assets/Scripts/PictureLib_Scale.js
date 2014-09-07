#pragma strict

var Wall : GameObject;

function OnMouseDown() {
	transform.parent.transform.Find("Zoom").gameObject.SetActive(true);
	var s : PictureLib = Wall.GetComponent.<PictureLib>();
	var pn = System.Int32.Parse(transform.Find("txt").GetComponent(TextMesh).text);
	
	if (!(GameObject.Find("Bootstrap").GetComponent.<Languages>().eng))
		transform.parent.transform.Find("Zoom/ZoomPicNum").GetComponent(TextMesh).text = "Рисунок "+pn;
	else transform.parent.transform.Find("Zoom/ZoomPicNum").GetComponent(TextMesh).text = "Picture "+pn;
	
	s.loadPicture(transform.parent.transform.Find("Zoom/ZoomPicture").gameObject, s.www[pn-1].texture, 0.9, 0.4);
}