function OnMouseDown() {
	
	
		
	
		var txt = transform.parent.transform.Find("Field Group").transform.Find("Field Text");
		print(txt.GetComponent(TextMesh).text);
	
}