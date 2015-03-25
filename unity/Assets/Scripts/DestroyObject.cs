using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    GameObject objectToDestroy;

    void OnSignal()
    {
        Destroy(objectToDestroy);
    }
}
