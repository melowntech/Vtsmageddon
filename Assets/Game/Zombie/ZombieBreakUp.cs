using UnityEngine;

public class ZombieBreakUp : MonoBehaviour
{
    public static void BreakUp(Transform p)
    {
        GameObject g = p.gameObject;
        g.tag = "Untagged";
        SkinnedMeshRenderer smr = g.GetComponent<SkinnedMeshRenderer>();
        if (smr)
        {
            g.AddComponent<MeshRenderer>().sharedMaterial = smr.sharedMaterial;
            g.AddComponent<MeshFilter>().mesh = smr.sharedMesh;
            g.AddComponent<SphereCollider>().radius = 0.1f;
            Rigidbody r = g.AddComponent<Rigidbody>();
            r.mass = 1;
            r.drag = 0.3f;
            r.angularDrag = 0.8f;
            Destroy(smr);
            Destroy(g, Random.Range(5f, 15f));
        }
        else
            Destroy(g);
        int cnt = p.childCount;
        for (int i = 0; i < cnt; i++)
            BreakUp(p.GetChild(i));
        p.transform.DetachChildren();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
            return;
        BreakUp(transform);
        ScoreLabel.killCount++;
    }
}
