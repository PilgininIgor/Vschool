using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
	
	public GameObject avatar;
	public Transform spawnObject;
	public string gameName = "3Ducation";
	
	//avatars
	public GameObject Joan;
	public GameObject Robot;
	public GameObject Alexis;
	public GameObject Mia;
	public GameObject Justin;
	public GameObject Vincent;
	public GameObject Solder;	
	
	public string nameOfAvatar;
	
	private bool refreshing;
	private HostData[] hostData;
	
	private float btnX;
	private float btnY;
	private float btnW;
	private float btnH;
	
	void Start () {
		btnX = Screen.width * 0.05f;
		btnY = Screen.width * 0.05f;
		btnW = Screen.width * 0.1f;
		btnH = Screen.width * 0.1f;
		nameOfAvatar = /*"Solder"*/CharacterCust.nameOfAvatar;
		Debug.Log("Avatar is " + nameOfAvatar);
		switch(nameOfAvatar)
		{
			case "Robot": avatar = Robot; break;
			case "Joan": avatar = Joan; break;
			case "Alexis": avatar = Alexis; break;
			case "Justin": avatar = Justin; break;
			case "Vincent": avatar = Vincent; break;
			case "Solder": avatar = Solder; break;
			case "Mia": avatar = Mia; break;
		}
        
	}
	
	void StartServer(){
		Network.InitializeServer(32, 25001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, "3Ducation", "This is the network version of the education");
	}
	
	void RefreshHostList(){
		MasterServer.RequestHostList(gameName);
		refreshing = true;		
	}
	
	void Update(){
		if(refreshing){
			if(MasterServer.PollHostList().Length > 0){
				refreshing = false;
				Debug.Log(MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList(); 
			}
		}
	}
	
	void SpawnPlayer(){
		Network.Instantiate(avatar, spawnObject.position, Quaternion.identity, 0);
	}
	
	void OnServerInitialized(){
		Debug.Log("Server initialized!");
		SpawnPlayer();
	}
	
	void OnConnectedToServer(){
		SpawnPlayer();
	}
	
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registered Server!");
		}
	}
	
	void OnGUI(){
		if(!Network.isClient && !Network.isServer) {
			if(GUI.Button(new Rect(btnX, btnY, btnW, btnH), "Start Server")){
				Debug.Log("Starting Server ");
				StartServer();	
				OrbitCam orbitCam = GameObject.Find("MainCamera").GetComponent("OrbitCam") as OrbitCam;
				orbitCam.target = GameObject.Find(nameOfAvatar + "(Clone)").transform;
			}
			
			if(GUI.Button(new Rect(btnX, btnY * 1.2f + btnH, btnW, btnH), "Refresh Hosts")){
				Debug.Log("Refreshing");
				RefreshHostList();
			}
			
			if(hostData != null){
				for(int i = 0; i < hostData.Length; i++){
					if(GUI.Button(new Rect(btnX * 1.5f + btnW, btnY * 1.2f + (btnH * i), btnW * 3, btnH * 0.5f), hostData[i].gameName)){
						Network.Connect(hostData[i]);
					}
				}
			}
		}
	}
	
}
