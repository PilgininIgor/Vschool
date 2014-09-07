private var order : boolean = true;
/*private var orderMouse : boolean = false;


//this.gameObject.GetComponent(TextMesh).text ="Click on plane ";
function OnMouseUp () {
    print("mouse");
     if (orderMouse==false ) {
	 this.gameObject.GetComponent(TextMesh).text="";
    order=true;
	}
}*/
 
 
function Update () {
    if (order) {
		for (var c : char in Input.inputString) {
		    
			if(this.gameObject.GetComponent(TextMesh).text.Length <17){
			// Backspace - Remove the last character
			if (c == "\b"[0]) {
				if (this.gameObject.GetComponent(TextMesh).text.Length != 0)
					this.gameObject.GetComponent(TextMesh).text = this.gameObject.GetComponent(TextMesh).text.Substring(0, this.gameObject.GetComponent(TextMesh).text.Length - 1);
			}
			// End of entry
			else if (c == "\n"[0] || c == "\r"[0]) {// "\n" for Mac, "\r" for windows.
				print ("Answer: " + this.gameObject.GetComponent(TextMesh).text);
				order=false;
				this.gameObject.GetComponent(TextMesh).text="Input ansver";
			}
			// Normal text input - just append to the end
			else {
				this.gameObject.GetComponent(TextMesh).text += c;
			}
			}
		}
    }
	
}