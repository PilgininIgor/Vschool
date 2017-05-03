using System.Collections;
using UnityEngine;

public class EffectSequencer : MonoBehaviour
{
    public class ExplosionPart
    {
        public GameObject gameObject = null;
        public float delay = 0;
        public bool hqOnly = false;
        public float yOffset = 0;
    }

    public ExplosionPart[] ambientEmitters, explosionEmitters, smokeEmitters, miscSpecialEffects;

    void Start()
    {
        float maxTime = 0;

        foreach (var go in ambientEmitters)
        {
            InstantiateDelayed(go);
            if (go.gameObject.GetComponent<ParticleEmitter>())
                maxTime = Mathf.Max(maxTime, go.delay + go.gameObject.GetComponent<ParticleEmitter>().maxEnergy);
        }
        foreach (var go in explosionEmitters)
        {
            InstantiateDelayed(go);
            if (go.gameObject.GetComponent<ParticleEmitter>())
                maxTime = Mathf.Max(maxTime, go.delay + go.gameObject.GetComponent<ParticleEmitter>().maxEnergy);
        }
        foreach (var go in smokeEmitters)
        {
            InstantiateDelayed(go);
            if (go.gameObject.GetComponent<ParticleEmitter>())
                maxTime = Mathf.Max(maxTime, go.delay + go.gameObject.GetComponent<ParticleEmitter>().maxEnergy);
        }

        if (GetComponent<AudioSource>() && GetComponent<AudioSource>().clip)
            maxTime = Mathf.Max(maxTime, GetComponent<AudioSource>().clip.length);

        foreach (var go in miscSpecialEffects)
        {
            InstantiateDelayed(go);
            if (go.gameObject.GetComponent<ParticleEmitter>())
                maxTime = Mathf.Max(maxTime, go.delay + go.gameObject.GetComponent<ParticleEmitter>().maxEnergy);
        }

        Destroy(gameObject, maxTime + 0.5f);
    }

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
    }

    void InstantiateDelayed(ExplosionPart go)
    {
        if (go.hqOnly && QualityManager.quality < QualityManager.Quality.High)
            return;
        StartCoroutine(Wait(go.delay));
        Instantiate(go.gameObject, transform.position + Vector3.up * go.yOffset, transform.rotation);
    }
}
