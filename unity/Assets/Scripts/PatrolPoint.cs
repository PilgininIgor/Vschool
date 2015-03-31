using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public Vector3 position;

    void Awake()
    {
        position = transform.position;
    }
}
