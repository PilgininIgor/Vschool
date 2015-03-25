using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool locked = true;

    SignalSender unlockedSignal;

    void OnSignal()
    {
        if (locked)
        {
            locked = false;
            unlockedSignal.SendSignals(this);
        }
    }
}
