using UnityEngine;

public class ReceiverItem {
	public GameObject receiver;
	public string action = "OnSignal";
    public float delay;
	
	public void SendWithDelay (MonoBehaviour sender) {
		//var t = new WaitForSeconds(delay);
		if (receiver)
			receiver.SendMessage (action);
		else
			Debug.LogWarning ("No receiver of signal \""+action+"\" on object "+sender.name+" ("+sender.GetType().Name+")", sender);
	}
}

public class SignalSender : MonoBehaviour {
	public bool onlyOnce;
	public ReceiverItem[] receivers;
	
	private bool hasFired = false;
	
	public void SendSignals (MonoBehaviour sender) {
	    if (hasFired != false && onlyOnce != false) return;
	    foreach (var t in receivers)
	    {
	        t.SendWithDelay(sender);
	    }
	    hasFired = true;
	}
}
