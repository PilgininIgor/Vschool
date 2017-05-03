using UnityEngine;
using System.Collections;

public class OpenBox2 : MonoBehaviour {
    public GameObject Board;
	public GameObject Christmas;
	public bool proverka=true;
	// Use this for initialization
	void Start () {
	//GameObject.Find("FlareChristmas").particleSystem.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider myTrigger)
	{
	  if (myTrigger.gameObject.name == "First Person Controller Prefab" && proverka)
	  {
        //Transform.Find("Flare Combo/Christmas Flare").ParticleSystem.Play();
		//GameObject a;
		//a=transform.Find("FlareCombo/ChristmasFlare");
		//a=transform.Find("box_top");
        //a.Play();
        //GameObject.Find("Christmas").ParticleSystem.Stop();
        //ParticleSystem waterGun;	
        //waterGun=GameObject.Find("Christmas"); 		
        //waterGun.Start();
		 //GameObject.Find("Christmas").GetComponent(particleSystem).Start();
		 //GameObject.Find("Flare").Find("Christmas").particleSystem.Stop();
         //GameObject.Find("Flare/Christmas").particleSystem.Stop();
		 //print("1111");
         ////GameObject.Find("FlareChristmas").particleSystem.Play();
		 Christmas.GetComponent<ParticleSystem>().Play();
         //WaitForSeconds(2);	 
		 //Foo();
		 StartCoroutine("Foo");
		 proverka=false;
		
		//GameObject.Find("Christmas").Start();
		//GameObject.Find("Christmas").Destroy();	
        //GameObject.Find("Christmas").transform.Rotate(0,0,0);				
	  }
	}
	
	private IEnumerator Foo()
    {
        
                yield return new WaitForSeconds(3);
				 Board.SetActive(true);	 
                
        
    }
	
}
