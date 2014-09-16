var i : int;
var is_active : boolean = false;
var animation_in_progress : boolean = false;

function Update () {
}

function AnimationStart() { animation_in_progress = true; }
function AnimationStop() { animation_in_progress = false; }

function SetAnswer(ans) {
	var scr = GameObject.Find("BoardGroup").GetComponent(BoardScript);
	if (name == "Cube1") scr.a[scr.i,0] = ans;
	else if (name == "Cube2") scr.a[scr.i,1] = ans;
	else if (name == "Cube3") scr.a[scr.i,2] = ans;
	else if (name == "Cube4") scr.a[scr.i,3] = ans;
	else if (name == "Cube5") scr.a[scr.i,4] = ans;
}

function OnMouseDown() {
	if (!animation_in_progress) {
		if (!is_active) {
			i = Random.Range(1, 5);
			if (i == 1) animation.Play("CubeAnim1");
			else if (i == 2) animation.Play("CubeAnim2");
			else if (i == 3) animation.Play("CubeAnim3");
			else if (i == 4) animation.Play("CubeAnim4");
			is_active = true; SetAnswer(1);
		} else {
			animation.Play("CubeReturn");
			is_active = false; SetAnswer(0);
		}
	}
}