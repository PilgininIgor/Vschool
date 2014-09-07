#pragma strict

var pic_enlarged = false;
var animation_in_progress = false;
var pic : Texture2D;

function AnimationStart() { animation_in_progress = true; }
function AnimationStop() { animation_in_progress = false; }

function LoadPicture() {
	var scr : Board = transform.parent.GetComponent.<Board>();
	var www = new WWW (scr.qPicPath[scr.i]);
	var Plane_RightButton = transform.parent.transform.Find("Plane_RightButton");
	yield www;
	Plane_RightButton.renderer.material.mainTexture = www.texture;
}

function UnloadPicture() {	
	var Plane_RightButton = transform.parent.transform.Find("Plane_RightButton");	
	Plane_RightButton.renderer.material.mainTexture = pic;
}

function OnMouseDown() {
	if ((!animation_in_progress) && (renderer.enabled)) {
		if (!pic_enlarged) {
			pic_enlarged = true;
			animation.Play("PictureAnimUp");
		} else {
			pic_enlarged = false;
			animation.Play("PictureAnimDown");
		}
	}	
}