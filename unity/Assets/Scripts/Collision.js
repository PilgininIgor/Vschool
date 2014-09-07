//этот скрипт нежно сдвигает персонажа вслед за стендом,
//если тот решил "свернуть" стенд, находясь внутри его пространства

var player : GameObject;
var movingForward = false;
var movingBackward = false;
var intersects = false;

private var prevPos = Vector3(0,0,0);
private var currPos = Vector3(0,0,0);
private var movement = Vector3(0,0,0);
private var firstmove = true;

function Update() {
	if (movingBackward) {
		if (firstmove) {currPos = transform.position; firstmove = false;}
		prevPos = currPos;
		currPos = transform.position;
		movement = currPos - prevPos;
		if (intersects) { player.transform.position += movement*1.2; }
	}
	else {
		if (!firstmove) firstmove = true;
	}	
}

function OnTriggerEnter(other:Collider) { intersects = true; }
function OnTriggerExit(other:Collider) { intersects = false; }