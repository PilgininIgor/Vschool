using UnityEngine;

public class RainsplashManager : MonoBehaviour
{

    public int numberOfParticles = 700;
    public float areaSize = 40.0f;
    public float areaHeight = 15;
    public float fallingSpeed = 23;
    public float flakeWidth = 0.4f;
    public float flakeHeight = 0.4f;
    public float flakeRandom = 0.1f;

    public Mesh[] preGennedMeshes;
    private int preGennedIndex = 0;

    public bool generateNewAssetsOnStart = false;

    public void Start()
    {
#if UNITY_EDITOR
        if (generateNewAssetsOnStart)
        {
            // create & save 3 meshes
            var m1 = CreateMesh();
            var m2 = CreateMesh();
            var m3 = CreateMesh();
            UnityEditor.AssetDatabase.CreateAsset(m1, "Assets/Objects/RainFx/" + gameObject.name + "_LQ0.asset");
            UnityEditor.AssetDatabase.CreateAsset(m2, "Assets/Objects/RainFx/" + gameObject.name + "_LQ1.asset");
            UnityEditor.AssetDatabase.CreateAsset(m3, "Assets/Objects/RainFx/" + gameObject.name + "_LQ2.asset");
            Debug.Log("Created new rainsplash meshes in Assets/Objects/RainFx/");
        }
#endif
    }

    public Mesh GetPreGennedMesh()
    {
        return preGennedMeshes[(preGennedIndex++) % preGennedMeshes.Length];
    }

    Mesh CreateMesh()
    {
        var mesh = new Mesh();
        // we use world space aligned and not camera aligned planes this time
        var cameraRight = transform.right * Random.Range(0.1f, 2.0f) + transform.forward * Random.Range(0.1f, 2.0f);// Vector3.forward;//Camera.main.transform.right;
        cameraRight = Vector3.Normalize(cameraRight);
        var cameraUp = Vector3.Cross(cameraRight, Vector3.up);
        cameraUp = Vector3.Normalize(cameraUp);

        int particleNum = QualityManager.quality > QualityManager.Quality.Medium ? numberOfParticles : numberOfParticles / 2;

        Vector3[] verts = new Vector3[4 * particleNum];
        Vector2[] uvs = new Vector2[4 * particleNum];
        Vector2[] uvs2 = new Vector2[4 * particleNum];
        Vector3[] normals = new Vector3[4 * particleNum];

        int[] tris = new int[2 * 3 * particleNum];

        Vector3 position;
        for (int i = 0; i < particleNum; i++)
        {
            int i4 = i * 4;
            int i6 = i * 6;

            position = new Vector3(areaSize * (Random.value - 0.5f), 0, areaSize * (Random.value - 0.5f));

            float rand = Random.value;
            float widthWithRandom = flakeWidth + rand * flakeRandom;
            float heightWithRandom = widthWithRandom;

            verts[i4 + 0] = position - cameraRight * widthWithRandom;// - 0.0 * heightWithRandom;
            verts[i4 + 1] = position + cameraRight * widthWithRandom;// - 0.0 * heightWithRandom;
            verts[i4 + 2] = position + cameraRight * widthWithRandom + cameraUp * 2.0f * heightWithRandom;
            verts[i4 + 3] = position - cameraRight * widthWithRandom + cameraUp * 2.0f * heightWithRandom;

            normals[i4 + 0] = -Camera.main.transform.forward;
            normals[i4 + 1] = -Camera.main.transform.forward;
            normals[i4 + 2] = -Camera.main.transform.forward;
            normals[i4 + 3] = -Camera.main.transform.forward;

            uvs[i4 + 0] = new Vector2(0.0f, 0.0f);
            uvs[i4 + 1] = new Vector2(1.0f, 0.0f);
            uvs[i4 + 2] = new Vector2(1.0f, 1.0f);
            uvs[i4 + 3] = new Vector2(0.0f, 1.0f);

            var tc1 = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            uvs2[i4 + 0] = new Vector2(tc1.x, tc1.y);
            uvs2[i4 + 1] = new Vector2(tc1.x, tc1.y); ;
            uvs2[i4 + 2] = new Vector2(tc1.x, tc1.y); ;
            uvs2[i4 + 3] = new Vector2(tc1.x, tc1.y); ;

            tris[i6 + 0] = i4 + 0;
            tris[i6 + 1] = i4 + 1;
            tris[i6 + 2] = i4 + 2;
            tris[i6 + 3] = i4 + 0;
            tris[i6 + 4] = i4 + 2;
            tris[i6 + 5] = i4 + 3;
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.uv2 = uvs2;
        mesh.RecalculateBounds();

        return mesh;
    }
}
