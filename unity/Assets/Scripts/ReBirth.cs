using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ReBirth : MonoBehaviour
{

    void Start()
    {
        var al = Camera.main.gameObject.GetComponent<AudioListener>();

        if (al)
            AudioListener.volume = 1.0f;

        var sm = GetComponent<ShaderDatabase>();
        StartCoroutine(sm.WhiteIn());

        camera.backgroundColor = Color.white;
    }
}
