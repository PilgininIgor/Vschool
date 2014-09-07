#pragma strict

var BoardStatic : GameObject; var BoardToMove : GameObject;
var Cube1 : GameObject; var Cube2 : GameObject; var Cube3 : GameObject; 
var Cube4 : GameObject; var Cube5 : GameObject; 
private var scr : Board;

function OnMouseDown () {
	BoardStatic.transform.localPosition.z = 17.95; //выдвинуть на вид заголовок вопроса и кнопки переключения
	BoardToMove.transform.Find("BoardGroup").animation.Play("BoardAppear"); //запустить вылет стенда
	
	this.transform.localPosition.z = 0.5;
	transform.parent.transform.Find("Result").localPosition.z = 0.5;
	transform.parent.transform.Find("Minimum").localPosition.z = 0.5;
	transform.parent.transform.Find("Conclusion").localPosition.z = 0.5;
		
	scr = BoardToMove.transform.Find("BoardGroup").GetComponent.<Board>();
	scr.i = 0; scr.initializeArrays(); scr.UpdateBeginning();
	
	//выдвинуть нужные кубы
	Cube1.animation.Play("CubeInactiveUp");
	Cube2.animation.Play("CubeInactiveUp");
	Cube3.animation.Play("CubeInactiveUp");
	if (scr.qAnsNum[0] > 3) Cube4.animation.Play("CubeInactiveUp");
	if (scr.qAnsNum[0] > 4) Cube5.animation.Play("CubeInactiveUp");	
}