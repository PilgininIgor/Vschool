#pragma strict

public var enterSignals : SignalSender;
public var exitSignals : SignalSender;
public var Lock : boolean;

public var objects : System.Collections.Generic.List.<GameObject>;

function Awake () {
	objects = new System.Collections.Generic.List.<GameObject> ();
	enabled = false;
}

function Locked() {
    Lock = true;
}

function OnTriggerEnter (other : Collider) {
	if (other.isTrigger)
		return;
	
	var wasEmpty : boolean = (objects.Count == 2);
	
	objects.Add (other.gameObject);
	
	if (wasEmpty) {
	    if (Lock) {
	        enterSignals.SendSignals(this);
	        enabled = true;
	    }
	}
}

function OnTriggerExit (other : Collider) {
	if (other.isTrigger)
		return;
	
	if (objects.Contains (other.gameObject))
		objects.Remove (other.gameObject);

    if (objects.Count == 2) {
        if (Lock) {
        }
        exitSignals.SendSignals(this);
        enabled = false;
    }
}
