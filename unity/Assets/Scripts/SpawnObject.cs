using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour
{

    GameObject objectToSpawn, spawned;
    SignalSender onDestroyedSignals;

    public SpawnObject()
    {
        enabled = false;
    }

    // When we get a signal, spawn the objectToSpawn and store the spawned object.
    // Also enable this behaviour so the Update function will be run.
    void OnSignal()
    {
        spawned = Spawner.Spawn(objectToSpawn, transform.position, transform.rotation);
        if (onDestroyedSignals.receivers.Length > 0)
            enabled = true;
    }

    // After the object is spawned, check each frame if it's still there.
    // Once it's not, activate the onDestroyedSignals and disable again.
    void Update()
    {
        if (spawned == null || spawned.active == false)
        {
            onDestroyedSignals.SendSignals(this);
            enabled = false;
        }
    }
}
