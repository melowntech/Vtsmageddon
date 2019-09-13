using UnityEngine;

public class DelayedRenderEnable : MonoBehaviour
{
    void Start()
    {
        foreach (var r in GetComponentsInChildren<MeshRenderer>())
            r.enabled = false;
        foreach (var r in GetComponentsInChildren<SkinnedMeshRenderer>())
            r.enabled = false;
    }

    void Update()
    {
        foreach (var r in GetComponentsInChildren<MeshRenderer>())
            r.enabled = true;
        foreach (var r in GetComponentsInChildren<SkinnedMeshRenderer>())
            r.enabled = true;
        Destroy(this);
    }
}
