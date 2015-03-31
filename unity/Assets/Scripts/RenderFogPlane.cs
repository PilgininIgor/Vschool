using UnityEngine;

[ExecuteInEditMode]
public class RenderFogPlane : MonoBehaviour
{

    public Camera cameraForRay;

    private Matrix4x4 frustumCorners;
    private float CAMERA_ASPECT_RATIO = 1.333333f, CAMERA_NEAR, CAMERA_FAR, CAMERA_FOV;

    private Mesh mesh;
    private Vector2[] uv = new Vector2[4];

    void OnEnable()
    {
        renderer.enabled = true;

        if (!mesh)
            mesh = GetComponent<MeshFilter>().sharedMesh;

        // write indices into uv's for fast world space reconstruction		

        if (mesh)
        {
            uv[0] = new Vector2(1.0f, 1.0f); // TR
            uv[1] = new Vector2(0.0f, 0.0f); // TL
            uv[2] = new Vector2(2.0f, 2.0f); // BR
            uv[3] = new Vector2(3.0f, 3.0f); // BL
            mesh.uv = uv;
        }

        if (!cameraForRay)
            cameraForRay = Camera.main;
    }

    private bool EarlyOutIfNotSupported()
    {
        if (Supported()) return false;
        enabled = false;
        return true;
    }

    void OnDisable()
    {
        renderer.enabled = false;
    }

    bool Supported()
    {
        return (renderer.sharedMaterial.shader.isSupported && SystemInfo.supportsImageEffects
            && SystemInfo.supportsRenderTextures && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth));
    }

    void Update()
    {
        if (EarlyOutIfNotSupported())
        {
            enabled = false;
            return;
        }
        if (!renderer.enabled)
            return;

        frustumCorners = Matrix4x4.identity;

        CAMERA_NEAR = cameraForRay.nearClipPlane;
        CAMERA_FAR = cameraForRay.farClipPlane;
        CAMERA_FOV = cameraForRay.fieldOfView;
        CAMERA_ASPECT_RATIO = cameraForRay.aspect;

        float fovWHalf = CAMERA_FOV * 0.5f;

        var toRight = cameraForRay.transform.right * CAMERA_NEAR * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * CAMERA_ASPECT_RATIO;
        var toTop = cameraForRay.transform.up * CAMERA_NEAR * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        var topLeft = (cameraForRay.transform.forward * CAMERA_NEAR - toRight + toTop);
        float CAMERA_SCALE = topLeft.magnitude * CAMERA_FAR / CAMERA_NEAR;

        // correctly place transform first

        var p = transform.localPosition;
        p.z = CAMERA_NEAR + 0.0001f;
        transform.localPosition = p;
        transform.localScale = new Vector3((toRight * 0.5f).magnitude, 1.0f, (toTop * 0.5f).magnitude);
        var r = transform.localRotation;
        r.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        transform.localRotation = r;

        // write view frustum corner "rays"

        topLeft.Normalize();
        topLeft *= CAMERA_SCALE;

        var topRight = (cameraForRay.transform.forward * CAMERA_NEAR + toRight + toTop);
        topRight.Normalize();
        topRight *= CAMERA_SCALE;

        var bottomRight = (cameraForRay.transform.forward * CAMERA_NEAR + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= CAMERA_SCALE;

        var bottomLeft = (cameraForRay.transform.forward * CAMERA_NEAR - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= CAMERA_SCALE;

        frustumCorners.SetRow(0, topLeft);
        frustumCorners.SetRow(1, topRight);
        frustumCorners.SetRow(2, bottomRight);
        frustumCorners.SetRow(3, bottomLeft);

        renderer.sharedMaterial.SetMatrix("_FrustumCornersWS", frustumCorners);
        renderer.sharedMaterial.SetVector("_CameraWS", cameraForRay.transform.position);
    }
}
