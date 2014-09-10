var firstline;

function OnMouseDown () {
	
	//ВЫВОД В БРАУЗЕР
	var scr = GameObject.Find("BoardGroup").GetComponent(BoardScript);
	var s : String[] = new String[scr.qText.Length];
	var i : int;
	
	for (i=0; i<scr.qText.Length; i++) {
		s[i] = "Q"+(i+1)+": "+scr.a[i,0]+" "+scr.a[i,1]+" "+scr.a[i,2];
		if (scr.qAnsNum[i] >= 4) s[i] = s[i]+" "+scr.a[i,3];
		if (scr.qAnsNum[i] == 5) s[i] = s[i]+" "+scr.a[i,4];
	}
	
	for (i=0; i<scr.qText.length; i++) {
		Application.ExternalEval("document.write('<p>"+s[i]+"</p>');");
	}
	
	//ВВОД ИЗ ТЕКСТОВОГО ФАЙЛА
	//кодировка файла должна быть в "UTF-8 без BOM", а не просто "UTF-8"
	//это важно, да, иначе числа не хотят парситься
	/*до лучших времен
	var scr = GameObject.Find("BoardGroup").GetComponent("BoardScript");
	var test : WWW = new WWW ("file://E:/Unity3D/Test.txt");
	yield test;
	var words = test.text.Split("\n"[0]);
	var k : int = System.Int32.Parse(words[0]);*/
}