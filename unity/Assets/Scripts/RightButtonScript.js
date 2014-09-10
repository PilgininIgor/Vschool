var pic_enlarged = false;
var animation_in_progress = false;

function AnimationStart() { animation_in_progress = true; }
function AnimationStop() { animation_in_progress = false; }

function LoadPicture() {
	var scr = GameObject.Find("BoardGroup").GetComponent(BoardScript);
	var www : WWW = new WWW (scr.qPicPath[scr.i]);
	yield www;
	GameObject.Find("Plane_RightButton").renderer.material.mainTexture = www.texture;
}

function UnloadPicture() {
	GameObject.Find("Plane_RightButton").renderer.material.mainTexture = Resources.Load("Picture");
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