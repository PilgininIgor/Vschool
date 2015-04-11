using System.Collections;
using UnityEngine;

[System.Serializable]
public class ReceiverItem {
	public GameObject receiver;
	public string action = "OnSignal";
    public float delay;

    public IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }
	
	public void SendWithDelay (MonoBehaviour sender)
	{
        sender.StartCoroutine(Wait(delay));
		if (receiver)
			receiver.SendMessage (action);
		else
			Debug.LogWarning ("No receiver of signal \""+action+"\" on object "+sender.name+" ("+sender.GetType().Name+")", sender);
	}
}

[System.Serializable]
public class SignalSender
{
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
