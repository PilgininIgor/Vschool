#pragma strict

var Wall : GameObject;

function OnMouseDown() {
	var scr : PictureLib = Wall.GetComponent.<PictureLib>();
	scr.current_page = scr.current_page + 1;
	if (scr.current_page > scr.pages_num) scr.current_page = 1;
	scr.loadPics(scr.current_page);
}