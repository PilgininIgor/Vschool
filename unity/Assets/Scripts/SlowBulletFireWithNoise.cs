using UnityEngine;

public class SlowBulletFireWithNoise : MonoBehaviour
{

    GameObject bulletPrefab;
    float frequency = 2, coneAngle = 1.5f;
    AudioClip fireSound;
    bool firing = false;
    float noisiness = 2.0f;

    private float nextFireNoise = 1.0f;
    private float lastFireTime = -1;


    void Update()
    {
        if (firing)
        {
            if (Time.time > nextFireNoise + lastFireTime + 1 / frequency)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        // Spawn visual bullet
        var coneRandomRotation = Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
        Spawner.Spawn(bulletPrefab, transform.position, transform.rotation * coneRandomRotation);

        if (audio && fireSound)
        {
            audio.clip = fireSound;
            audio.Play();
        }

        lastFireTime = Time.time;
        nextFireNoise = Random.value * noisiness;
    }

    void OnStartFire()
    {
        firing = true;
    }

    void OnStopFire()
    {
        firing = false;
    }
}
