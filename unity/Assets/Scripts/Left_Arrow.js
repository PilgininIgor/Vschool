#pragma strict

var BoardGroup : GameObject;
var Cube1 : GameObject;
var Cube2 : GameObject;
var Cube3 : GameObject;
var Cube4 : GameObject;
var Cube5 : GameObject;
var Plane_RightButton : GameObject;

function OnMouseDown() {
	
	var scr1 : Cube  = Cube1.GetComponent.<Cube>();
	var scr2 : Cube  = Cube2.GetComponent.<Cube>();
	var scr3 : Cube  = Cube3.GetComponent.<Cube>();
	var scr4 : Cube  = Cube4.GetComponent.<Cube>();
	var scr5 : Cube  = Cube5.GetComponent.<Cube>();
	var scrb = Plane_RightButton.GetComponent.<Right_Button>();
	
	if ((!scr1.animation_in_progress) && (!scr2.animation_in_progress)
	&& (!scr3.animation_in_progress) && (!scr4.animation_in_progress)
	&& (!scr5.animation_in_progress) && (!scrb.animation_in_progress)) {
	
		var scr : Board = BoardGroup.GetComponent.<Board>();
		var anim = BoardGroup.GetComponent(Animation);
		var anim1 = Cube1.GetComponent(Animation);
		var anim2 = Cube2.GetComponent(Animation);
		var anim3 = Cube3.GetComponent(Animation);
		var anim4 = Cube4.GetComponent(Animation);
		var anim5 = Cube5.GetComponent(Animation);
		
		//расчет времени ответа на вопрос
		scr.t[scr.i] += Time.timeSinceLevelLoad - scr.prev_time;
		scr.prev_time = Time.timeSinceLevelLoad;
						
		scr.i = scr.i - 1;
		if (scr.i < 0) scr.i = scr.qText.length - 1;
		anim.Play("BoardAnim");
		
		if (scrb.pic_enlarged) {
			var animb = Plane_RightButton.GetComponent(Animation);
			animb.Play("PictureAnimDown");
			scrb.pic_enlarged = false;
		}
		
		if (scr1.is_active) anim1.Play("CubeActiveDown"); else anim1.Play("CubeInactiveDown");
		if (scr2.is_active) anim2.Play("CubeActiveDown"); else anim2.Play("CubeInactiveDown");
		if (scr3.is_active) anim3.Play("CubeActiveDown"); else anim3.Play("CubeInactiveDown");
		if (scr4.is_active) anim4.Play("CubeActiveDown"); else anim4.Play("CubeInactiveDown");
		if (scr5.is_active) anim5.Play("CubeActiveDown"); else anim5.Play("CubeInactiveDown");
		
	}		
	
}