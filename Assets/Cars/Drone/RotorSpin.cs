using UnityEngine;

public class RotorSpin : MonoBehaviour
{
    private Transform tr;
    private MeshFilter mf;
    private MeshRenderer mr;
    private Mesh slowMesh;
    private Material slowMaterial;
    public Mesh fastMesh;
    public Material fastMaterial;
    public float threshold = 150;

    void Start()
    {
        tr = GetComponent<Transform>();
        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
        slowMesh = mf.sharedMesh;
        slowMaterial = mr.sharedMaterial;
        fastMaterial = Instantiate(fastMaterial);
    }

    public void Spin(float angle)
    {
        tr.localEulerAngles = tr.localEulerAngles + new Vector3(0, angle, 0);
        angle = Mathf.Abs(angle);
        bool fast = angle > threshold;
        mf.sharedMesh = fast ? fastMesh : slowMesh;
        mr.sharedMaterial = fast ? fastMaterial : slowMaterial;
        if (fast)
        {
            var c = fastMaterial.color;
            c.a = Mathf.Clamp01(angle / threshold - 0.9f);
            fastMaterial.color = c;
        }
    }
}
