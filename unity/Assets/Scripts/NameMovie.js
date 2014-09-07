 //var hdr = new Array();

var hdr = new Array(
	"1) 3Ducation Демо",
	"2) Переменнные",
	"3) Типы данных",
	"4) Линейный алгоритм",
	"5) Ветвление"
);


 var movie = new Array (
	"file://D:/Video/UnityDemo.ogv",
	"file://D:/Video/Peremen.ogv",
	"file://D:/Video/TipeDate.ogv",
	"file://D:/Video/LineAlgorim.ogv",
	"file://D:/Video/Vetvlen.ogv"
); 

/* var movie = new Array (
	"Video/Video1.ogv",
	"Video/Video2.ogv",
	"Video/Video1.ogv",
	"Video/Video2.ogv",
	"Video/Video1.ogv",
	"Video/Video2.ogv"
); */
 

var n1:int;
var all:int;
all=hdr.length-2;

function Start() {
	var list1 = transform.parent.transform.Find("List1").gameObject;
	var list2 = transform.parent.transform.Find("List2").gameObject;
	var list3 = transform.parent.transform.Find("List3").gameObject;
	var list4 = transform.parent.transform.Find("List4").gameObject;
	list1.GetComponent(TextMesh).text = hdr[n1]; //list1.AddComponent(BoxCollider);
	list2.GetComponent(TextMesh).text = hdr[n1+1]; //list2.AddComponent(BoxCollider);
	list3.GetComponent(TextMesh).text = hdr[n1+2]; //list2.AddComponent(BoxCollider);
	list4.GetComponent(TextMesh).text = hdr[n1+3]; //list2.AddComponent(BoxCollider);
	}