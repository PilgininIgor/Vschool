function OnMouseDown() {
	
	var scr1 = GameObject.Find("Cube1").GetComponent(CubeScript);
	var scr2 = GameObject.Find("Cube2").GetComponent(CubeScript);
	var scr3 = GameObject.Find("Cube3").GetComponent(CubeScript);
	var scr4 = GameObject.Find("Cube4").GetComponent(CubeScript);
	var scr5 = GameObject.Find("Cube5").GetComponent(CubeScript);
	var scrb = GameObject.Find("Plane_RightButton").GetComponent(RightButtonScript);
	
	if ((!scr1.animation_in_progress) && (!scr2.animation_in_progress)
	&& (!scr3.animation_in_progress) && (!scr4.animation_in_progress)
	&& (!scr5.animation_in_progress) && (!scrb.animation_in_progress)) {
	
		var scr = GameObject.Find("BoardGroup").GetComponent(BoardScript);
		var anim = GameObject.Find("BoardGroup").GetComponent(Animation);
		var anim1 = GameObject.Find("Cube1").GetComponent(Animation);
		var anim2 = GameObject.Find("Cube2").GetComponent(Animation);
		var anim3 = GameObject.Find("Cube3").GetComponent(Animation);
		var anim4 = GameObject.Find("Cube4").GetComponent(Animation);
		var anim5 = GameObject.Find("Cube5").GetComponent(Animation);
	
		scr.i = scr.i + 1;
		if (scr.i >= scr.qText.length) scr.i = 0;
		anim.Play("BoardAnim");
		
		if (scrb.pic_enlarged) {
			var animb = GameObject.Find("Plane_RightButton").GetComponent(Animation);
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