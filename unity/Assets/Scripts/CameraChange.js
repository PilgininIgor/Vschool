//не забудьте добавить box collider к плейну и настроить его размеры
//(они и будут областью, при захождении в которому всплывет кнопка с лупой)
//а еще назначить в инспекторе слой "Ignore Raycast" этому плейну и персонажу,
//чтобы ни тот, ни другой не загораживали коллайдеры кнопок "Назад/Далее" и фоток
//и не мешали по ним кликать

#pragma strict

var MainCam : Camera;
var StandCam : Camera;
var PlayerAvatar : GameObject;
var magnifier : Texture;
var arrow : Texture;

var HallwayGroup : GameObject;
var addInfo = "";

var hint_visible = false;
var escape_visible = false;

StandCam.enabled = false; 
StandCam.GetComponent(AudioListener).enabled = false;

function setIsBound(isBound: boolean)
{
/*Need to fix for multiplayer!!!*/
	if (!PlayerAvatar)
		PlayerAvatar = GameObject.Find("Bootstrap")/*.GetComponent(NetworkManagerScript).avatar*/;
		
	PlayerAvatar.SetActive(!isBound);
	/*if (isBound)
	{
		PlayerAvatar.animation.Stop();
	}
	else
	{
		PlayerAvatar.animation.Play("idle");
	}*/
	
	MainCam.enabled = !isBound; 
	MainCam.GetComponent(AudioListener).enabled = !isBound;
	
	StandCam.enabled = isBound; 
	StandCam.GetComponent(AudioListener).enabled = isBound;
	
	hint_visible = !isBound; 
	escape_visible = isBound;
	
	if (!isBound)
	{
		HallwayGroup.GetComponent.<HallwayAchievements>().Check(transform.parent.name, addInfo);
	}
}

function ZoomIn() 
{	
	setIsBound(true);
}

function ZoomOut() 
{
	setIsBound(false);
}

function OnGUI() {
	if (hint_visible) 
		if (GUI.Button(Rect(10,10,100,100), magnifier)) 
			ZoomIn(); 
			
	if (escape_visible) 
		if (GUI.Button(Rect(10,10,100,100), arrow)) 
			ZoomOut();
}

function OnTriggerEnter(other: Collider) 
{
	//print(other.gameObject.name);
	hint_visible = true;
}

function OnTriggerExit(other: Collider) 
{
	hint_visible = false;
}