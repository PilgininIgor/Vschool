﻿#pragma strict

function Start () {

}

//function Update () {

//if (Input.GetKeyDown (KeyCode.W))
    
//Screen.lockCursor = true;
//Screen.showCursor = true;

function Update() {
   if (Input.GetKeyDown(KeyCode.E) && UnityEngine.Cursor.visible == false) { 
      UnityEngine.Cursor.visible = true;
      Screen.lockCursor = false;
   }else if(Input.GetKeyDown(KeyCode.E)){
     UnityEngine.Cursor.visible = false;
     Screen.lockCursor = true;
  }
}


//guiTexture.enabled = true;


//Screen.lockCursor = true;
//Screen.lockCursor = false;


//Cursor.position = this.PointToScreen(new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2));

//}