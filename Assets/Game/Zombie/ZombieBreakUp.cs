using UnityEngine;

public class ZombieBreakUp : MonoBehaviour
{
    private bool broken = false;

    private static void BreakUp(SkinnedMeshRenderer smr)
    {
        GameObject g = smr.gameObject;
        g.tag = "Untagged";
        g.AddComponent<MeshRenderer>().sharedMaterial = smr.sharedMaterial;
        g.AddComponent<MeshFilter>().mesh = smr.sharedMesh;
        var mc = g.AddComponent<MeshCollider>();
        mc.sharedMesh = smr.sharedMesh;
        mc.convex = true;
        Rigidbody r = g.AddComponent<Rigidbody>();
        r.mass = 2;
        Destroy(smr);
        Destroy(g, Random.Range(5f, 15f));
        g.transform.SetParent(null);
    }

    public static void BreakUp(Transform p)
    {
        GameObject g = p.gameObject;
        foreach (SkinnedMeshRenderer smr in g.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            BreakUp(smr);
        Destroy(g);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
            return;
        if (broken)
            return;
        broken = true;
        BreakUp(transform);
        ScoreLabel.AddScore();
    }
}
