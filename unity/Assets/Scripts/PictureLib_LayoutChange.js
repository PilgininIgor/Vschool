#pragma strict

var Wall : GameObject;

function OnMouseDown() {
	var s : PictureLib = Wall.GetComponent.<PictureLib>();
	for (var i=0; i<s.stand.Count; i++) Destroy(s.stand[i]);
	s.cl = s.cl + 1; if (s.cl >= s.layout_x.length) s.cl = 0;
	s.CreateLayout(s.layout_x[s.cl], s.layout_y[s.cl]);
	s.loadPics(1); s.current_page = 1;
}
