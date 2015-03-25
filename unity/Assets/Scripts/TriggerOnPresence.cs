using System.Collections.Generic;
using UnityEngine;

public class TriggerOnPresence : MonoBehaviour
{

    public SignalSender enterSignals, exitSignals;
    public bool Lock;

    public List<GameObject> objects;
    void Awake()
    {
        objects = new List<GameObject>();
        enabled = false;
    }

    void Locked()
    {
        Lock = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        bool wasEmpty = (objects.Count == 2);

        objects.Add(other.gameObject);

        if (wasEmpty)
        {
            if (Lock)
            {
                enterSignals.SendSignals(this);
                enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;

        if (objects.Contains(other.gameObject))
            objects.Remove(other.gameObject);

        if (objects.Count == 2)
        {
            if (Lock)
            {
            }
            exitSignals.SendSignals(this);
            enabled = false;
        }
    }
}
