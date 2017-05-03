using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ShaderDatabase : MonoBehaviour
{

    public Shader[] shaders;
    public bool cookShadersOnMobiles = true;
    public Material cookShadersCover;
    private GameObject cookShadersObject;

    void Awake()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        Screen.sleepTimeout = 0;

        if (!cookShadersOnMobiles)
            return;

        if (!cookShadersCover.HasProperty("_TintColor"))
            Debug.LogWarning("Dualstick: the CookShadersCover material needs a _TintColor property to properly hide the cooking process", transform);

        CreateCameraCoverPlane();
        cookShadersCover.SetColor("_TintColor", new Color(0, 0, 0, 1));
#endif
    }

    GameObject CreateCameraCoverPlane()
    {
        cookShadersObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cookShadersObject.GetComponent<Renderer>().material = cookShadersCover;
        cookShadersObject.transform.parent = transform;
        cookShadersObject.transform.localPosition = new Vector3 { z = 1.55f };
        cookShadersObject.transform.localRotation = Quaternion.identity;
        var r = cookShadersObject.transform.localEulerAngles;
        r.z += 180;
        cookShadersObject.transform.localEulerAngles = r;
        var s = Vector3.one * 1.5f;
        s.x *= 1.6f;
        cookShadersObject.transform.localScale = s;

        return cookShadersObject;
    }

    IEnumerator WhiteOut()
    {
        CreateCameraCoverPlane();
        var mat = cookShadersObject.GetComponent<Renderer>().sharedMaterial;
        mat.SetColor("_TintColor", new Color(1.0f, 1.0f, 1.0f, 0));

        var c = new Color(1.0f, 1.0f, 1.0f, 0);
        while (c.a < 1.0)
        {
            c.a += Time.deltaTime * 0.25f;
            mat.SetColor("_TintColor", c);
            yield return c;
        }

        DestroyCameraCoverPlane();
    }

    public IEnumerator WhiteIn()
    {
        CreateCameraCoverPlane();
        var mat = cookShadersObject.GetComponent<Renderer>().sharedMaterial;
        mat.SetColor("_TintColor", new Color(1.0f, 1.0f, 1.0f, 1.0f));


        var c = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        while (c.a > 0.0)
        {
            c.a -= Time.deltaTime * 0.25f;
            mat.SetColor("_TintColor", c);
            yield return c;
        }

        DestroyCameraCoverPlane();
    }

    void DestroyCameraCoverPlane()
    {
        if (cookShadersObject)
            DestroyImmediate(cookShadersObject);
        cookShadersObject = null;
    }

    void Start()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        if (cookShadersOnMobiles)
            StartCoroutine(CookShaders());
#endif
    }

    // this function is cooking all shaders to be used in the game. 
    // it's good practice to draw all of them in order to avoid
    // triggering in game shader compilations which might cause evil
    // frame time spikes

    // currently only enabled for mobile (iOS and Android) platforms

    IEnumerator CookShaders()
    {
        if (shaders.Length > 0)
        {
            var m = new Material(shaders[0]);
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.parent = transform;
            cube.transform.localPosition = new Vector3 { z = 4.0f };

            foreach (var s in shaders)
            {
                if (s)
                {
                    m.shader = s;
                    cube.GetComponent<Renderer>().material = m;
                }
                yield return s;
            }

            Destroy(m);
            Destroy(cube);

            var c = Color.black;
            c.a = 1.0f;
            while (c.a > 0.0)
            {
                c.a -= Time.deltaTime * 0.5f;
                cookShadersCover.SetColor("_TintColor", c);
                yield return c;
            }
        }

        DestroyCameraCoverPlane();
    }
}
