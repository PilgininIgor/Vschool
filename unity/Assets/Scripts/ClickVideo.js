

function OnMouseDown(){
var www : WWW;
yield www;
var PlaneBoardMaterial = GameObject.Find("Plane_Board").GetComponent(Renderer);
var PlaneBoardAudio = GameObject.Find("Plane_Board").GetComponent(AudioSource);
var s1 = transform.parent.transform.Find("Plane").GetComponent(NameMovie);
       
	 
		
		if(name=="List1"){
		/*www = new WWW (s1.movie[s1.n1]);
	    yield www;
	    GameObject.Find("Plane_Board").renderer.material.mainTexture = www.movie;
		PlaneBoardMaterial.material.mainTexture.Play();
		
		PlaneBoardAudio.audio.clip=PlaneBoardMaterial.material.mainTexture.audioClip;
		PlaneBoardAudio.audio.Play();*/
		//renderer.material.SetColor('black');
		renderer.material.color = Color.red;
		gameObject.Find("List2").renderer.material.color = Color.white;
		gameObject.Find("List3").renderer.material.color = Color.white;
		gameObject.Find("List4").renderer.material.color = Color.white;
		} 
		
		if(name=="List2"){
		/*www = new WWW (s1.movie[s1.n1+1]);
	    yield www;
	    GameObject.Find("Plane_Board").renderer.material.mainTexture = www.movie;
		PlaneBoardMaterial.material.mainTexture.Play();
		PlaneBoardAudio.audio.clip=PlaneBoardMaterial.material.mainTexture.audioClip;
		PlaneBoardAudio.audio.Play();*/
		renderer.material.color = Color.red;
		gameObject.Find("List1").renderer.material.color = Color.white;
		gameObject.Find("List3").renderer.material.color = Color.white;
		gameObject.Find("List4").renderer.material.color = Color.white;
		} 
		
		if(name=="List3"){
		/*www = new WWW (s1.movie[s1.n1+2]);
	    yield www;
	    GameObject.Find("Plane_Board").renderer.material.mainTexture = www.movie;
		PlaneBoardMaterial.material.mainTexture.Play();
		//audio.clip = renderer.material.mainTexture.audioClip;
		//audio.Play();
		PlaneBoardAudio.audio.clip=PlaneBoardMaterial.material.mainTexture.audioClip;
		PlaneBoardAudio.audio.Play();*/
		renderer.material.color = Color.red;
		gameObject.Find("List1").renderer.material.color = Color.white;
		gameObject.Find("List2").renderer.material.color = Color.white;
		gameObject.Find("List4").renderer.material.color = Color.white;
		} 
		
		if(name=="List4"){
		/*www = new WWW (s1.movie[s1.n1+3]);
	    yield www;
	    GameObject.Find("Plane_Board").renderer.material.mainTexture = www.movie;
		PlaneBoardMaterial.material.mainTexture.Play();
		//	audio.clip = renderer.material.mainTexture.audioClip;
		//audio.Play();		
		PlaneBoardAudio.audio.clip=PlaneBoardMaterial.material.mainTexture.audioClip;
		PlaneBoardAudio.audio.Play();*/
		renderer.material.color = Color.red;
		gameObject.Find("List1").renderer.material.color = Color.white;
		gameObject.Find("List2").renderer.material.color = Color.white;
		gameObject.Find("List3").renderer.material.color = Color.white;
		} 
}