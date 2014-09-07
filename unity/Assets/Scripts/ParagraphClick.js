//скрипт клика по параграфу висит на всех четырех параграфах, а также на кнопке "Вернуться к списку параграфов"
//их действия немного разные, поэтому далее можно найти проверки вида name=="", которые уточняют, к кому
//из пяти возможных объектов принадлежит этот скрипт
#pragma strict

function OnMouseDown () {
	if (name!="BackButton") {
		transform.parent.transform.Find("P1").gameObject.active = false;
		transform.parent.transform.Find("P2").gameObject.active = false;
		transform.parent.transform.Find("P3").gameObject.active = false;
		transform.parent.transform.Find("P4").gameObject.active = false;
		transform.parent.transform.Find("BackButton").gameObject.active = true;
		
		var s1 = transform.parent.transform.Find("Stand").GetComponent.<TextLib>();
		var s2 = transform.parent.transform.parent.transform.Find("PicturesGroup/Wall").GetComponent.<PictureLib>();
		var sd = transform.parent.transform.Find("Stand").GetComponent.<ParagraphList>();
		transform.parent.animation.Play("StandMove1");
		if (name=="P1") {
			s1.orderNumber = sd.p[0].orderNumber;
			s1.Display(sd.p[0].header, sd.p[0].text, 0.08, 0.06, 0.8, 4, 8, ">", -0.02);
			s2.DisplayPictures(sd.p[0].pictures);			
		}
		else if (name=="P2") {
			s1.orderNumber = sd.p[1].orderNumber;
			s1.Display(sd.p[1].header, sd.p[1].text, 0.08, 0.06, 0.8, 4, 8, ">", -0.02);
			s2.DisplayPictures(sd.p[1].pictures);			
		}
		else if (name=="P3") {
			s1.orderNumber = sd.p[2].orderNumber;
			s1.Display(sd.p[2].header, sd.p[2].text, 0.08, 0.06, 0.8, 4, 8, ">", -0.02);
			s2.DisplayPictures(sd.p[2].pictures);			
		}
		else if (name=="P4") {
			s1.orderNumber = sd.p[3].orderNumber;
			s1.Display(sd.p[3].header, sd.p[3].text, 0.08, 0.06, 0.8, 4, 8, ">", -0.02);
			s2.DisplayPictures(sd.p[3].pictures);			
		}		
	} else {
		transform.parent.transform.Find("P1").gameObject.active = true;
		transform.parent.transform.Find("P2").gameObject.active = true;
		transform.parent.transform.Find("P3").gameObject.active = true;
		transform.parent.transform.Find("P4").gameObject.active = true;
		transform.parent.transform.Find("BackButton").gameObject.active = false;
		
		s1 = transform.parent.transform.Find("Stand").GetComponent.<TextLib>();
		s2 = transform.parent.transform.parent.transform.Find("PicturesGroup/Wall").GetComponent.<PictureLib>();
		transform.parent.animation.Play("StandMove2");
		s1.Undisplay();
		s2.UndisplayPictures();
	}
}