#pragma strict

class Course {
	var name : String;
	var themes : List.<Theme>;
}

class Theme {
	var name : String;
	var contents : List.<ThemeContent>;
}

class ThemeContent {
	var id : String;
	var name : String;
	var type : String;
	var questions : List.<Question>;
	var paragraphs : List.<Paragraph>;
}

class Question {
	var text : String; //текст вопроса
	var picQ : String; //путь к картинке-пояснению либо null, если оной нет
	var if_pictured : boolean; //true, если ответы картинкой, false - если текстом
	var picA : String; //путь к ответам картинкой (if_pictured=true) либо null (if_pictured=false)
	var ans_count : int; //количество вариантов ответа (да, если они текстом, то это известно и через answers.Count, а вот если картинкой, то нет)
	var answers : List.<Answer>; //список вопросов текстом (очевидно пуст, если if_pictured=true)
}

class Answer {
	var text : String;	
}

class Paragraph {
	var orderNumber : int;
	var header : String;
	var text : String;
	var pictures : List.<Picture>;
}

class Picture {
	var path : String;
}

//--------------

class CourseRun {
	var id : String;
	var mode : String;
	var name : String;
	var progress : float;
	var timeSpent : float;
	var themesRuns : List.<ThemeRun>;	
	
	var visited : boolean;
	var completeAll : boolean;
}

class ThemeRun {
	var id : String;
	var name : String;
	var progress : float;
	var testsComplete : int;
	var testsOverall : int;
	var timeSpent : float;
	var testsRuns : List.<TestRun>;
	var lecturesRuns : List.<LectureRun>;
	
	var allLectures: boolean;
	var allTests : boolean;
	var allTestsMax : boolean;
	var completeAll : boolean;
}

//здесь будет только по одному (лучшему) результату для каждого теста, а не все попытки, все попытки останутся на сервере
class TestRun {
	var answersMinimum : int;
	var answersCorrect : int;
	var answersOverall : int;	
}

class LectureRun {
	var timeSpent : float;
	var paragraphsRuns : List.<ParagraphRun>;
}

class ParagraphRun {
	var haveSeen : boolean;
}

//--------------

class OverallRPG {
	var ifGuest : boolean;
	var username : String;
	
	var EXP : int;
	
	//стартовые ачивменты - холл - стенды
	var facultyStands_Seen : boolean;
	var facultyStands_Finish : boolean;
	var historyStand_Seen : boolean;
	var historyStand_Finish : boolean;
	var scienceStand_Seen : boolean;
	var scienceStand_Finish : boolean;
	var staffStand_Seen : boolean;
	var staffStand_Finish : boolean;
	
	//стартовые ачивменты - холл - остальное
	var logotypeJump : boolean;
	var tableJump : boolean;
	var terminalJump : boolean;
	var ladderJump_First : boolean;
	var ladderJump_All : boolean;
	var letThereBeLight : boolean;
	
	//стартовые ачивменты - коридоры
	var plantJump_First : boolean;
	var plantJump_Second : boolean;	
	var barrelRoll : boolean;
	
	var firstVisitLecture : boolean;
	var firstVisitTest : boolean;
	var teleportations : int;
	var paragraphsSeen : int;
	var testsFinished : int;
}