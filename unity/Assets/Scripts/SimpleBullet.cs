using UnityEngine;

public class SimpleBullet : MonoBehaviour
{

    float speed = 10, lifeTime = 0.5f, dist = 10000, spawnTime = 0;
    private Transform tr;

    void OnEnable()
    {
        tr = transform;
        spawnTime = Time.time;
    }

    void Update()
    {
        tr.position += tr.forward * speed * Time.deltaTime;
        dist -= speed * Time.deltaTime;
        if (Time.time > spawnTime + lifeTime || dist < 0)
        {
            Destroy(gameObject);
        }
    }
}
