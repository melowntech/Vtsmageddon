using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private NavMeshAgent agent;
    private uint pathUpdateDelay = 0;
    private uint standingCounter = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = Mathf.Pow(Random.value, 0.5f) * 20;
        //agent.updatePosition = false;
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!agent.isOnNavMesh || (transform.position - player.transform.position).magnitude > 100)
        {
            Destroy(gameObject); // destroy this zombie
            return;
        }
        if (pathUpdateDelay++ % 120 == 13)
            agent.SetDestination(player.transform.position);
        if (!agent.hasPath)
            return;
        Vector3 move = agent.desiredVelocity;
        if (move.magnitude > 1)
            move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);
        bool close = agent.remainingDistance < agent.stoppingDistance + 0.1;
        if (close || move.magnitude < 0.1f)
        {
            move = Vector3.zero;
            if (standingCounter++ > 1500 && Random.value < 0.0001f)
            {
                KillPart(transform);
                return;
            }
        }
        else
            standingCounter = 0;
        animator.SetFloat("forward", move.z);
        animator.SetFloat("turn", move.x);
        if (close)
        {
            if (Random.value < 0.003f)
                animator.SetBool("attack", true);
        }
        else
            animator.SetBool("attack", false);
    }

    static void KillPart(Transform p)
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
            KillPart(p.GetChild(i));
        p.transform.DetachChildren();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
            return;
        KillPart(transform);
        ScoreLabel.killCount++;
    }
}
