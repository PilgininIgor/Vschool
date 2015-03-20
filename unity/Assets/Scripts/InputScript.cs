using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputScript : MonoBehaviour
{
    public int theme_num;
    public int lec_num;

    public void generateLecture (DataStructures.ThemeContent lecture, int theme_num1, int lec_num1) {
	theme_num = theme_num1; lec_num = lec_num1; //чтобы знать, к каким массивам статистики STAT обращаться
			
	//сначала мы равномерно распределяем параграфы по стендам, записывая итог в массив pnum
	//то есть 7 параграфов будут распределены по одному на стенд => pnum = (1,1,1,1,1,1,1)
	//9 параграфов - (2,2,1,1,1,1,1)
	//20 параграфов - (3,3,3,3,3,3,2)
	//и так далее
	var div = lecture.paragraphs.Count/7;
	var mod = lecture.paragraphs.Count % 7;
	//print(lecture.paragraphs.Count); print(div); print(mod);
	int[] pnum = new int[7];
	for (int i = 0; i<mod; i++) pnum[i] = pnum[i] + 1;
	for (int i=0; i<7; i++) pnum[i]+=div;
	//for (i=0; i<7; i++) print(pnum);
	
	//потом мы раскидываем ссылки на параграфы по временным массивам (p[0] для первого стенда,
	//p[1] для второго и т.д.), чтобы было удобнее перекидывать их следующим скриптам
	var p = new List<DataStructures.Paragraph[]>();
	var k = 0;
	for (int i=0; i<7; i++) {
		p.Add(new DataStructures.Paragraph[pnum[i]]);
		for (var j=0; j<pnum[i]; j++) {
			p[i][j] = lecture.paragraphs[k];
			k++;
		}
	}
	
	//потом мы включаем и инициализируем либо отключаем те или иные объекты в зависимости от количества параграфов
	//blank - это группа с пустыми стенами; оные стены будут заменять стенды, которым не перепало ни одного параграфа
	ParagraphList scr;
	switch (lecture.paragraphs.Count) {
		case 0:
			//если параграфов нет вообще, выключаем все стенды, кроме 4го (того, что напротив входа),
			//а у него выключаем скрипт и выводим дежурную надпись
			transform.Find("blank/1").gameObject.active = true; transform.Find("blank/2").gameObject.active = true;
			transform.Find("blank/3").gameObject.active = true;	transform.Find("blank/5").gameObject.active = true;
			transform.Find("blank/6").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("2").gameObject.SetActive(false);
			transform.Find("3").gameObject.SetActive(false); transform.Find("5").gameObject.SetActive(false);
			transform.Find("6").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			
			transform.Find("blank/4").gameObject.active = false;
			transform.Find("4").gameObject.SetActive(true);
			transform.Find("4/StandGroup/BackButton").gameObject.active = false;
			
			transform.Find("4/StandGroup/Stand").GetComponent<ParagraphList>().enabled = false;
			//transform.Find("4/StandGroup/P1").GetComponent(TextMesh).text = "Why so empty?";
		break;
		case 1:
			//если параграф один, то действуем аналогично, только вместо дежурной надписи будет полноценный параграф
			transform.Find("blank/1").gameObject.SetActive(true); transform.Find("blank/2").gameObject.active = true;
			transform.Find("blank/3").gameObject.active = true;	transform.Find("blank/5").gameObject.active = true;
			transform.Find("blank/6").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("2").gameObject.SetActive(false);
			transform.Find("3").gameObject.SetActive(false); transform.Find("5").gameObject.SetActive(false);
			transform.Find("6").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			
			transform.Find("blank/4").gameObject.active = false;
			transform.Find("4").gameObject.SetActive(true);
			transform.Find("4/StandGroup/BackButton").gameObject.active = false;
			transform.Find("4/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			//вот она, инициализация - передаем ссылки на нужные параграфы в скрипт ParagraphList
			//и выводим на стенд заголовки этих параграфов
			scr = transform.Find("4/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
		break;
		case 2:
			//2 параграфа - по одному на стенды 3 и 5, остальные выключаем
			transform.Find("blank/1").gameObject.active = true; transform.Find("blank/2").gameObject.active = true;
			transform.Find("blank/6").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("blank/4").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("2").gameObject.SetActive(false);
			transform.Find("6").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			transform.Find("4").gameObject.SetActive(false);
			
			transform.Find("blank/3").gameObject.active = false;
			transform.Find("3").gameObject.SetActive(true);
			transform.Find("3/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/5").gameObject.active = false;
			transform.Find("5").gameObject.SetActive(true);
			transform.Find("5/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
		break;
		case 3:
			//3 параграфа - по одному на стенды 3, 4 и 5, остальные выключаем
			transform.Find("blank/1").gameObject.active = true; transform.Find("blank/2").gameObject.active = true;
			transform.Find("blank/6").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("2").gameObject.SetActive(false);
			transform.Find("6").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			
			transform.Find("blank/3").gameObject.active = false;
			transform.Find("3").gameObject.SetActive(true);
			transform.Find("3/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/4").gameObject.active = false;
			transform.Find("4").gameObject.SetActive(true);
			transform.Find("4/StandGroup/BackButton").gameObject.active = false;
			transform.Find("4/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/5").gameObject.active = false;
			transform.Find("5").gameObject.SetActive(true);
			transform.Find("5/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("4/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[2]; scr.Paragraphs(scr.p);
		break;
		case 4:
			//4 параграфа - по одному на стенды 2, 3, 5 и 6, остальные выключаем
			transform.Find("blank/1").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("blank/4").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			transform.Find("4").gameObject.SetActive(false);
			
			transform.Find("blank/2").gameObject.active = false;
			transform.Find("2").gameObject.SetActive(true);
			transform.Find("2/StandGroup/BackButton").gameObject.active = false;
			transform.Find("2/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/3").gameObject.active = false;
			transform.Find("3").gameObject.SetActive(true);
			transform.Find("3/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/5").gameObject.active = false;
			transform.Find("5").gameObject.SetActive(true);
			transform.Find("5/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/6").gameObject.active = false;
			transform.Find("6").gameObject.SetActive(true);
			transform.Find("6/StandGroup/BackButton").gameObject.active = false;
			transform.Find("6/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("2/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[2]; scr.Paragraphs(scr.p);
			scr = transform.Find("6/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[3]; scr.Paragraphs(scr.p);
		break;
		case 5:
			//5 параграфов - по одному на стенды 2-6, а 1 и 7 выключаем
			transform.Find("blank/1").gameObject.active = true;	transform.Find("blank/7").gameObject.active = true;
			transform.Find("1").gameObject.SetActive(false); transform.Find("7").gameObject.SetActive(false);
			
			transform.Find("blank/2").gameObject.active = false;
			transform.Find("2").gameObject.SetActive(true);
			transform.Find("2/StandGroup/BackButton").gameObject.active = false;
			transform.Find("2/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/3").gameObject.active = false;
			transform.Find("3").gameObject.SetActive(true);
			transform.Find("3/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/4").gameObject.active = false;
			transform.Find("4").gameObject.SetActive(true);
			transform.Find("4/StandGroup/BackButton").gameObject.active = false;
			transform.Find("4/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/5").gameObject.active = false;
			transform.Find("5").gameObject.SetActive(true);
			transform.Find("5/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/6").gameObject.active = false;
			transform.Find("6").gameObject.SetActive(true);
			transform.Find("6/StandGroup/BackButton").gameObject.active = false;
			transform.Find("6/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("2/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
			scr = transform.Find("4/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[2]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[3]; scr.Paragraphs(scr.p);
			scr = transform.Find("6/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[4]; scr.Paragraphs(scr.p);
		break;
		case 6:
			//6 параграфов - выключаем 4, на всех остальных по одному
			transform.Find("blank/4").gameObject.active = true;
			transform.Find("4").gameObject.SetActive(false);
			
			transform.Find("blank/1").gameObject.active = false;
			transform.Find("1").gameObject.SetActive(true);
			transform.Find("1/StandGroup/BackButton").gameObject.active = false;
			transform.Find("1/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/2").gameObject.active = false;
			transform.Find("2").gameObject.SetActive(true);
			transform.Find("2/StandGroup/BackButton").gameObject.active = false;
			transform.Find("2/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/3").gameObject.active = false;
			transform.Find("3").gameObject.SetActive(true);
			transform.Find("3/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/5").gameObject.active = false;
			transform.Find("5").gameObject.SetActive(true);
			transform.Find("5/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/6").gameObject.active = false;
			transform.Find("6").gameObject.SetActive(true);
			transform.Find("6/StandGroup/BackButton").gameObject.active = false;
			transform.Find("6/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("blank/7").gameObject.active = false;
			transform.Find("7").gameObject.SetActive(true);
			transform.Find("7/StandGroup/BackButton").gameObject.active = false;
			transform.Find("7/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("1/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("2/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[2]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[3]; scr.Paragraphs(scr.p);
			scr = transform.Find("6/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[4]; scr.Paragraphs(scr.p);
			scr = transform.Find("7/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[5]; scr.Paragraphs(scr.p);
		break;
		default:
			//7 или больше - значит, все стенды будут заняты и действуем по общей схеме
			transform.Find("blank").gameObject.SetActive(false);
			transform.Find("1").gameObject.SetActive(true); transform.Find("2").gameObject.SetActive(true);
			transform.Find("3").gameObject.SetActive(true); transform.Find("4").gameObject.SetActive(true);
			transform.Find("5").gameObject.SetActive(true); transform.Find("6").gameObject.SetActive(true);
			transform.Find("7").gameObject.SetActive(true);
			transform.Find("1/StandGroup/BackButton").gameObject.active = false; transform.Find("2/StandGroup/BackButton").gameObject.active = false;
			transform.Find("3/StandGroup/BackButton").gameObject.active = false; transform.Find("4/StandGroup/BackButton").gameObject.active = false;
			transform.Find("5/StandGroup/BackButton").gameObject.active = false; transform.Find("6/StandGroup/BackButton").gameObject.active = false;
			transform.Find("7/StandGroup/BackButton").gameObject.active = false;
			transform.Find("1/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("2/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("3/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("4/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("5/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("6/PicturesGroup/Zoom").gameObject.SetActive(false);
			transform.Find("7/PicturesGroup/Zoom").gameObject.SetActive(false);
			
			scr = transform.Find("1/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[0]; scr.Paragraphs(scr.p);
			scr = transform.Find("2/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[1]; scr.Paragraphs(scr.p);
			scr = transform.Find("3/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[2]; scr.Paragraphs(scr.p);
			scr = transform.Find("4/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[3]; scr.Paragraphs(scr.p);
			scr = transform.Find("5/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[4]; scr.Paragraphs(scr.p);
			scr = transform.Find("6/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[5]; scr.Paragraphs(scr.p);
			scr = transform.Find("7/StandGroup/Stand").GetComponent<ParagraphList>();
			scr.p = p[6]; scr.Paragraphs(scr.p);
		break;
	}
}
}
