//аналогично TextLib, сам скрипт ничего не делает, а просто предоставляет для внешнего вызова функции
//DisplayPictures и UndisplayPictures
#pragma strict

var bckg : Material;

var www : List.<WWW> = List.<WWW>();
var stand : List.<GameObject> = new List.<GameObject>();
var pages_num : int;
var pics_per_page : int;
var current_page : int;

var layout_x : int[] = [1,2,1,2,3,1,3,2,3,4,2,4,3,4];
var layout_y : int[] = [1,1,2,2,1,3,2,3,3,2,4,3,4,4];
var cl : int; //current_layout

var w_max;
var h_max;

//создать стенд, а точнее, группу из трех объектов: задний фон back с материалом bckg,
//место под картинку pic и в нижнем правом углу текст txt с порядковым номером картинки
function createStand(scale:Vector3, pos:Vector3) {
	var obj : GameObject; var pn : GameObject;
	var back : GameObject; var pic : GameObject; var txt : GameObject;
	obj = new GameObject("pic_group");
	pn = transform.parent.transform.Find("PicNumber").gameObject;
	back = Instantiate(this.gameObject);
	pic = Instantiate(this.gameObject);
	txt = Instantiate(pn.gameObject);
	back.name = "back"; pic.name = "pic"; txt.name = "txt";
		
	obj.transform.parent = transform.parent.transform;
	back.transform.parent = obj.transform;
	pic.transform.parent = obj.transform;
	txt.transform.parent = obj.transform;
	
	obj.transform.localPosition = pos;
	obj.transform.localRotation = Quaternion.identity;
	back.transform.localPosition = Vector3.zero - Vector3(0.01, 0, 0);
	pic.transform.localPosition = Vector3.zero - Vector3(0.02, 0, 0);
	txt.transform.localPosition = Vector3.zero - Vector3(0.02, 0, 0);
	
	back.transform.localScale = scale; pic.transform.localScale = scale; 
	back.renderer.material = bckg;  pic.renderer.material = bckg;
	txt.transform.localPosition.y -= back.transform.localScale.z/2*10 - 0.3;
	txt.transform.localPosition.z -= back.transform.localScale.x/2*10 - 0.2;
		
	obj.AddComponent(BoxCollider);
	obj.GetComponent(BoxCollider).size = Vector3(0.1, scale.z*10, scale.x*10);
	obj.AddComponent("PictureLib_Scale");
	obj.GetComponent.<PictureLib_Scale>().Wall = this.gameObject;
		
	return obj;
}

//загрузить картинку на ранее созданный стенд obj, отмасштабировав ее так,
//чтобы она уместилась в области w_max на h_maх и в то же время сохранила пропорции
function loadPicture(obj:GameObject, texture:Texture2D, w_max:float, h_max:float) {
	obj.renderer.material.mainTexture = texture;
	var w : float = texture.width;
	var h : float = texture.height;
	if ((w/w_max) >= (h/h_max)) {
		obj.transform.localScale.x = w_max;
		obj.transform.localScale.z = w_max / w * h;
	}
	else {
		obj.transform.localScale.x = h_max / h * w;
		obj.transform.localScale.z = h_max;
	}
}

//создать компоновку x на y стендов
function CreateLayout(x:int, y:int) {
	var w_all = this.transform.localScale.x - 0.1;
	var h_all = this.transform.localScale.z - 0.1;
	var w = w_all / x;
	var h = h_all / y;
	var x_start = w_all * 10 / 2; //домножаем на 10, т.к. масштаб плейна - это обычные координаты 1-к-10
	var y_start = h_all * 10 / 2;
	
	var k = 0;
	for (var j=0; j<y; j++)
		for (var i=0; i<x; i++) {
			stand[k] = createStand(Vector3(w-0.01, 1, h-0.01),
								   Vector3(0, y_start-h*10/2-j*h*10, x_start-w*10/2-i*w*10));
			k++;
	}
	
	w_max = w-0.01;
	h_max = h-0.01;
	pics_per_page = x*y;
	pages_num = Mathf.Ceil(www.Count/1.0/(x*y));
	//у деления на 1.0 есть глубокий функциональный смысл: он добавляет float в операцию трех int'ов
	//если его убрать, то результат тоже будет целым (например, 14/(2*2)=3), и никакой Ceil не поможет
}

//загрузить на текущую компоновку картинки, соответствующие номеру страницы page
//например, при компоновке 1-на-1 и странице №12 надо загрузить картинку №12
//при компоновке 2-на-2 и странице №2 - картинки 5,6,7,8
function loadPics(page:int) {
	var i : int;
	var stand_length : int = layout_x[cl]*layout_y[cl];
	if (page != pages_num) {
		for (i=0; i<stand_length; i++) {
			stand[i].SetActive(true);
			loadPicture(stand[i].transform.Find("pic").gameObject,
						www[(page-1)*pics_per_page + i].texture,
						w_max, h_max);
			stand[i].transform.Find("txt").GetComponent(TextMesh).text = "" + ((page-1)*pics_per_page + i + 1);
		}				
	//последняя страница уникальна тем, что может быть неполной
	//(например, компоновка 2-на-2 и всего 14 картинок - тогда на последней будут картинки 13 и 14,
	//а место под 15 и 16 останется пустым)
	//поэтому ей и выделен отдельный else
	} else {
		var pics_on_last_page : int = www.Count - (pages_num-1)*pics_per_page;
		for (i=0; i < pics_on_last_page; i++) {
			loadPicture(stand[i].transform.Find("pic").gameObject,
						www[(page-1)*pics_per_page + i].texture,
						w_max, h_max);
			stand[i].transform.Find("txt").GetComponent(TextMesh).text = "" + ((page-1)*pics_per_page + i + 1);
		}				
		for (i=pics_on_last_page; i<stand_length; i++) stand[i].SetActive(false);
	}
}

function DisplayPictures(input : List.<Picture>) {
	if (input.Count == 0) {
		transform.parent.transform.Find("LayoutButton").gameObject.active = false;
		transform.parent.transform.Find("Tip").gameObject.active = false;
		transform.parent.transform.Find("Left Arrow").gameObject.active = false;
		transform.parent.transform.Find("Right Arrow").gameObject.active = false;
		transform.parent.transform.Find("Zoom").gameObject.SetActive(false);
	}
	else {
		//суть >>>
		for (var i=0; i<input.Count; i++) {
			www[i] = new WWW(input[i].path);
			yield www[i];
		}
		CreateLayout(1, 1); cl = 0;
		loadPics(1); current_page = 1;
		// <<< суть
	}	
}

function UndisplayPictures() {
	for (var i=0; i<stand.Count; i++) Destroy(stand[i]);
	transform.parent.transform.Find("Zoom").gameObject.SetActive(false);
}