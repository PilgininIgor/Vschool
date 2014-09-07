#pragma strict

//import DataStructures;

//массивы с информацией о вопросах (одинаковый индекс - одинаковый вопрос)
var qText : String[]; //текст вопроса
var qPicPath = new Array(); //путь к картинке для вопроса либо 0, если таковой не предусмотрено
var qType = new Array(); //тип ответов: 0 - текстом, 1 - картинкой
var qAns : String[]; //ответы текстом (тип = 0) либо путь к картинке с ними (тип = 1)
var qAnsNum : int[]; //количество вариантов ответа

var a : List.<int[]>; //двумерный массив двоичных значений, запоминающий все ответы пользователя
//a[i][j] = true/false - пользователь выбрал/не выбрал j-тый вариант ответа на i-тый вопрос
var t : List.<int> = new List.<int>(); //одномерный массив секунд, которые считают время ответа на каждый вопрос
//t[i] = 125.55 - i-тый вопрос был активен (выведен на стенд) в течение 2 минут и 5.55 секунд
var i : int = 0; //номер текущего вопроса (нумеруются с нуля)
var prev_time : float;
var test_id : String;
var theme_num : int;
var test_num : int;

//это функция инициализации, ее должен предварительно вызвать внешний скрипт
function generateTest(test: ThemeContent, theme_num1 : int, test_num1 : int) {
	theme_num = theme_num1;
	test_num = test_num1;
	if (test.questions.Count == 0) {
		//если вопросов нет, то превращаем кнопку в обычную надпись, т.к.
		//рассчитаны на присутствие хотя бы одного полноценного вопроса
		//transform.parent.transform.parent.transform.Find("Board Begin/Button").GetComponent(TextMesh).text = "Why so empty?";
		Destroy(transform.parent.transform.parent.transform.Find("Board Begin/Button").GetComponent(BoxCollider));		
	}
	else {
		//если все нормально, то переписываем данные из иерархии во внутренние массивы
		//да, работать с той же иерархией по ссылкам, а не плодить переменные, было бы лучше,
		//но тестовая комната разрабатывалась заранее, а менять скрипты в надцатный раз ну совсем нехочеццо
		test_id = test.id;
		qAnsNum = new int[test.questions.Count];
		qAns = new String[test.questions.Count];
		qText = new String[test.questions.Count];
		for (var l=0; l<test.questions.Count; l++) {
			qText[l] = DivideText(test.questions[l].text);
			qAnsNum[l] = test.questions[l].ans_count;
			if (test.questions[l].picQ == null) qPicPath[l] = "0"; else qPicPath[l] = test.questions[l].picQ;
			if (test.questions[l].if_pictured) {
				qType[l] = 1;
				qAns[l] = test.questions[l].picA;
			} else {
				qType[l] = 0;
				qAns[l] = "";
				for (var u=0; u<qAnsNum[l]-1; u++)
					qAns[l] += test.questions[l].answers[u].text + "\n";
				qAns[l] += test.questions[l].answers[qAnsNum[l]-1].text;
			}	
		}
		initializeArrays(); UpdateBeginning();
	}	
}

function DivideText(str : String) {
	var s = str.Split(" "[0]);
	var t = transform.Find("Text_Question").gameObject;
	var width = transform.Find("Plane_Board").transform.localScale.x*10;
	var first = 0;
	var i = 0;
	
	while (i<s.length-1) {
		t.GetComponent(TextMesh).text = "";
		for (var j=first; j<=i; j++) t.GetComponent(TextMesh).text += s[j];
		t.GetComponent(TextMesh).text += " " + s[i+1];
		if (t.GetComponent(MeshRenderer).bounds.size.x <= width) {
			s[i] += " ";
		} else {
			s[i] += "\n";
			first = i+1;
		}
		i++;
	}
			
	t.GetComponent(TextMesh).text = "";
	var res = "";
	for (i=0; i<s.length; i++) res += s[i];
	return res;
}

function initializeArrays() {
	var j : int; var k : int;
	a = new List.<int[]>();
	for (j=0; j<qText.length; j++) {
		a.Add(new int[qAnsNum[j]]);
		for (k=0; k<qAnsNum[j]; k++)
			a[j][k] = 0;
	}
	for (j=0; j<qText.length; j++)
		t.Add(0.00);
}

var Text_Header : GameObject;
var CubeGroup1 : GameObject;
var CubeGroup2 : GameObject;
var CubeGroup3 : GameObject;
var CubeGroup4 : GameObject;
var CubeGroup5 : GameObject;

function UpdateBeginning() {
	prev_time = Time.timeSinceLevelLoad;
	
	transform.Find("Text_Question").GetComponent(TextMesh).text = qText[i];
	if (!(GameObject.Find("Bootstrap").GetComponent.<Languages>().eng))
		Text_Header.GetComponent(TextMesh).text = "Вопрос №"+(i+1);
	else Text_Header.GetComponent(TextMesh).text = "Question №"+(i+1);
	
	if (qPicPath[i] == "0") transform.Find("Plane_RightButton").renderer.enabled = false;
	else transform.Find("Plane_RightButton").renderer.enabled = true;
	
	switch (qAnsNum[i]) {
		case 3:
			CubeGroup4.transform.Find("Cube4").renderer.enabled = false;
			CubeGroup5.transform.Find("Cube5").renderer.enabled = false;
			CubeGroup1.transform.localPosition.x = -2;
			CubeGroup2.transform.localPosition.x = 0;
			CubeGroup3.transform.localPosition.x = 2;
			break;
		case 4:
			CubeGroup4.transform.Find("Cube4").renderer.enabled = true;
			CubeGroup5.transform.Find("Cube5").renderer.enabled = false;
			CubeGroup1.transform.localPosition.x = -3;
			CubeGroup2.transform.localPosition.x = -1;
			CubeGroup3.transform.localPosition.x = 1;
			CubeGroup4.transform.localPosition.x = 3;
			break;
		case 5:
			CubeGroup4.transform.Find("Cube4").renderer.enabled = true;
			CubeGroup5.transform.Find("Cube5").renderer.enabled = true;
			CubeGroup1.transform.localPosition.x = -4;
			CubeGroup2.transform.localPosition.x = -2;
			CubeGroup3.transform.localPosition.x = 0;
			CubeGroup4.transform.localPosition.x = 2;
			CubeGroup5.transform.localPosition.x = 4;
			break;
	}
}

function UpdateQuestion() {
	UpdateBeginning();
	
	transform.Find("Plane_LeftButton").GetComponent.<Left_Button>().looking_at_question = true;
	
	transform.Find("Plane_LeftButton").transform.Find("Answers").gameObject.active = true;
	transform.Find("Plane_LeftButton").transform.Find("Question").gameObject.active = false;
	transform.Find("Text_Question").renderer.enabled = true;
	transform.Find("Plane_Pic_Answers").renderer.enabled = false;
			
	if (a[i][0] == 0) { 
		CubeGroup1.transform.Find("Cube1").animation.Play("CubeInactiveUp");
		CubeGroup1.transform.Find("Cube1").GetComponent.<Cube>().is_active = false;
	} else {
		CubeGroup1.transform.Find("Cube1").animation.Play("CubeActiveUp");
		CubeGroup1.transform.Find("Cube1").GetComponent.<Cube>().is_active = true;
	}
	if (a[i][1] == 0) {
		CubeGroup2.transform.Find("Cube2").animation.Play("CubeInactiveUp");
		CubeGroup2.transform.Find("Cube2").GetComponent.<Cube>().is_active = false;
	} else {
		CubeGroup2.transform.Find("Cube2").animation.Play("CubeActiveUp");
		CubeGroup2.transform.Find("Cube2").GetComponent.<Cube>().is_active = true;
	}	
	if (a[i][2] == 0) {
		CubeGroup3.transform.Find("Cube3").animation.Play("CubeInactiveUp");
		CubeGroup3.transform.Find("Cube3").GetComponent.<Cube>().is_active = false;
	} else {
		CubeGroup3.transform.Find("Cube3").animation.Play("CubeActiveUp");
		CubeGroup3.transform.Find("Cube3").GetComponent.<Cube>().is_active = true;
	}	
	if ((qAnsNum[i] == 4) || (qAnsNum[i] == 5)) {
		if (a[i][3] == 0) {
			CubeGroup4.transform.Find("Cube4").animation.Play("CubeInactiveUp");
			CubeGroup4.transform.Find("Cube4").GetComponent.<Cube>().is_active = false;
		} else {
			CubeGroup4.transform.Find("Cube4").animation.Play("CubeActiveUp");
			CubeGroup4.transform.Find("Cube4").GetComponent.<Cube>().is_active = true;
		}	
	}	
	if (qAnsNum[i] == 5) {
		if (a[i][4] == 0) {
			CubeGroup5.transform.Find("Cube5").animation.Play("CubeInactiveUp");
			CubeGroup5.transform.Find("Cube5").GetComponent.<Cube>().is_active = false;
		} else {
			CubeGroup5.transform.Find("Cube5").animation.Play("CubeActiveUp");
			CubeGroup5.transform.Find("Cube5").GetComponent.<Cube>().is_active = true;
		}	
	}
}