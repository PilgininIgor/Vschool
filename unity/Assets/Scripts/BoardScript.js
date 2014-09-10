//массивы с информацией о вопросах (одинаковый индекс - одинаковый вопрос)
var SIZE : int = 6;
var qText : String[] = new String[SIZE]; //текст вопроса
var qPicPath : String[] = new String[SIZE]; //путь к картинке для вопроса либо 0, если таковой не предусмотрено
var qType : int[] = new int[SIZE]; //тип ответов: 0 - текстом, 1 - картинкой
var qAns : String[] = new String[SIZE]; //ответы текстом (тип = 0) либо путь к картинке с ними (тип = 1)
var qAnsNum : int[] = new int[SIZE]; //количество вариантов ответа

var a : int[,] = new int[SIZE,SIZE];//двумерный массив двоичных значений, запоминающий все ответы пользователя
//a[i][j] = true/false - пользователь выбрал/не выбрал j-тый вариант ответа на i-тый вопрос
var i = 0; //номер текущего вопроса (нумеруются с нуля)

//ИНИЦИАЛИЗАЦИЯ.....................................
generateTest(); initializeAnsArray(); UpdateBeginning();
function generateTest() {
	qText[0] = "Даны числа A и B, записанные в разных системах счисления."+"\n"+
		"А = 9D (16-ричная система)"+"\n"+
		"B = 237 (8-ричная система)"+"\n"+
		"Какое из чисел С, записанных в двоичной системе,"+"\n"+
		"отвечает условию A<C<B?";
				
	qText[1] = "В некоторой стране автомобильный номер состоит из 7 символов."+"\n"+
		"В качестве символов используют 18 различных букв и десятичные"+"\n"+
		"цифры в любом порядке."+"\n"+
		"Каждый такой номер в компьютерной программе записывается"+"\n"+
		"минимально возможным и одинаковым целом количеством байтов,"+"\n"+
		"при этом используют посимвольное кодирование и все символы"+"\n"+
		"кодируются одинаковым и минимально возможным количеством битов."+"\n"+
		"Определите объем памяти, отводимый этой программой для записи"+"\n"+
		"60 номеров.";
		
	qText[2] = 'На рисунке представлена часть кодовой таблицы ASCII.'+"\n"+
		'Каков шестнадцатеричный код символа "q"?';
		
	qText[3] = "Вычислите сумму чисел X и Y. Результат представьте в двоичном виде."+"\n"+
		"X = 110111 (двоичная система)"+"\n"+
		"X = 135 (восьмеричная система)";
	
	qText[4] = "Определите значение переменной c после выполнения"+"\n"+
		"фрагмента программы, представленного на рисунке"+"\n"+
		"(фрагмент записан на разных языках программирования).";
		
	qText[5] = "В программе используется одномерный целочисленный массив А"+"\n"+
		"с индексами от 0 до 10. На рисунке представлен фрагмент программы,"+"\n"+
		"записанный на разных языках программирования, в котором значения"+"\n"+
		"элементов сначала задаются, а затем меняются."+"\n"+
		"Чему будут равны элементы этого массива после выполнения"+"\n"+
		"фрагмента программы?";
		
	qAnsNum[0] = 3; qAnsNum[1] = 4; qAnsNum[2] = 5;
	qAnsNum[3] = 3; qAnsNum[4] = 5; qAnsNum[5] = 4;
	
	qType[0] = 0; qType[1] = 0; qType[2] = 0;
	qType[3] = 0; qType[4] = 0; qType[5] = 1;
	
	qAns[0] = "1) 10011010"+"\n"+"2) 10011110"+"\n"+"3) 10011111";
	qAns[1] = "1) 240 байт"+"\n"+"2) 300 байт"+"\n"+"3) 360 байт"+"\n"+"4) 420 байт";
	qAns[2] = "1) 71"+"\n"+"2) 83"+"\n"+"3) А1"+"\n"+"4) В3"+"\n"+"5) FF";
	qAns[3] = "1) 11010100"+"\n"+"2) 10100100"+"\n"+"3) 10010100";
	qAns[4] = "1) с = 20"+"\n"+"2) с = 70"+"\n"+"3) с = -20"+"\n"+
				   "4) с = 180"+"\n"+"5) с = 220";
	qAns[5] = "file://E:/Unity3D/a6.jpg";
	
	qPicPath[0] = "0";
	qPicPath[1] = "0";
	qPicPath[2] = "pics/q3.jpg"; //qPicPath[2] = "file://E:/Unity3D/q3.jpg";
	qPicPath[3] = "0";
	qPicPath[4] = "pics/q5.jpg"; //qPicPath[4] = "file://E:/Unity3D/q5.jpg";
	qPicPath[5] = "pics/q6.jpg"; //qPicPath[5] = "file://E:/Unity3D/q6.jpg";
}

function initializeAnsArray() {
	var j : int; var k : int;
	for (j=0; j<qText.Length; j++) {
		for (k=0; k<qAnsNum[j]; k++)
			a[j,k] = 0;
	}		
}

//.....................................ИНИЦИАЛИЗАЦИЯ

function UpdateBeginning() {
	GameObject.Find("Text_Question").GetComponent(TextMesh).text = qText[i];
	GameObject.Find("Text_Header").GetComponent(TextMesh).text = "Вопрос №"+(i+1);
	
	if (qPicPath[i] == 0) GameObject.Find("Plane_RightButton").renderer.enabled = false;
	else GameObject.Find("Plane_RightButton").renderer.enabled = true;
	
	switch (qAnsNum[i]) {
		case 3:
			GameObject.Find("Cube4").GetComponent(Renderer).enabled = false;
			GameObject.Find("Cube5").GetComponent(Renderer).enabled = false;
			GameObject.Find("CubeGroup1").GetComponent(Transform).position.x = -2;
			GameObject.Find("CubeGroup2").GetComponent(Transform).position.x = 0;
			GameObject.Find("CubeGroup3").GetComponent(Transform).position.x = 2;
			break;
		case 4:
			GameObject.Find("Cube4").GetComponent(Renderer).enabled = true;
			GameObject.Find("Cube5").GetComponent(Renderer).enabled = false;
			GameObject.Find("CubeGroup1").GetComponent(Transform).position.x = -3;
			GameObject.Find("CubeGroup2").GetComponent(Transform).position.x = -1;
			GameObject.Find("CubeGroup3").GetComponent(Transform).position.x = 1;
			GameObject.Find("CubeGroup4").GetComponent(Transform).position.x = 3;
			break;
		case 5:
			GameObject.Find("Cube4").GetComponent(Renderer).enabled = true;
			GameObject.Find("Cube5").GetComponent(Renderer).enabled = true;
			GameObject.Find("CubeGroup1").GetComponent(Transform).position.x = -4;
			GameObject.Find("CubeGroup2").GetComponent(Transform).position.x = -2;
			GameObject.Find("CubeGroup3").GetComponent(Transform).position.x = 0;
			GameObject.Find("CubeGroup4").GetComponent(Transform).position.x = 2;
			GameObject.Find("CubeGroup5").GetComponent(Transform).position.x = 4;
			break;
	}
}

function UpdateQuestion() {
	UpdateBeginning();
	
	GameObject.Find("Plane_LeftButton").GetComponent("LeftButtonScript").looking_at_question = true;
	GameObject.Find("Plane_LeftButton").renderer.material = Resources.Load("button_answers");
	GameObject.Find("Text_Question").renderer.enabled = true;
	GameObject.Find("Plane_Pic_Answers").renderer.enabled = false;
			
	if (a[i,0] == 0) { 
		GameObject.Find("Cube1").animation.Play("CubeInactiveUp");
		GameObject.Find("Cube1").GetComponent("CubeScript").is_active = false;
	} else {
		GameObject.Find("Cube1").animation.Play("CubeActiveUp");
		GameObject.Find("Cube1").GetComponent("CubeScript").is_active = true;
	}
	if (a[i,1] == 0) {
		GameObject.Find("Cube2").animation.Play("CubeInactiveUp");
		GameObject.Find("Cube2").GetComponent("CubeScript").is_active = false;
	} else {
		GameObject.Find("Cube2").animation.Play("CubeActiveUp");
		GameObject.Find("Cube2").GetComponent("CubeScript").is_active = true;
	}	
	if (a[i,2] == 0) {
		GameObject.Find("Cube3").animation.Play("CubeInactiveUp");
		GameObject.Find("Cube3").GetComponent("CubeScript").is_active = false;
	} else {
		GameObject.Find("Cube3").animation.Play("CubeActiveUp");
		GameObject.Find("Cube3").GetComponent("CubeScript").is_active = true;
	}	
	if ((qAnsNum[i] == 4) || (qAnsNum[i] == 5)) {
		if (a[i,3] == 0) {
			GameObject.Find("Cube4").animation.Play("CubeInactiveUp");
			GameObject.Find("Cube4").GetComponent("CubeScript").is_active = false;
		} else {
			GameObject.Find("Cube4").animation.Play("CubeActiveUp");
			GameObject.Find("Cube4").GetComponent("CubeScript").is_active = true;
		}	
	}	
	if (qAnsNum[i] == 5) {
		if (a[i,4] == 0) {
			GameObject.Find("Cube5").animation.Play("CubeInactiveUp");
			GameObject.Find("Cube5").GetComponent("CubeScript").is_active = false;
		} else {
			GameObject.Find("Cube5").animation.Play("CubeActiveUp");
			GameObject.Find("Cube5").GetComponent("CubeScript").is_active = true;
		}	
	}
}