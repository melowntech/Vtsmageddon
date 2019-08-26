using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private NavMeshAgent agent;
    private int counter = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = Random.Range(4f, 7f);
        //agent.updatePosition = false;
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (counter++ % 30 == 13)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void Update()
    {
        if (!agent.isOnNavMesh || (transform.position - player.transform.position).magnitude > 100)
        {
            Destroy(gameObject); // destroy this zombie
            return;
        }
        if (!agent.hasPath)
            return;
        Vector3 move = agent.desiredVelocity;
        if (move.magnitude > 1)
            move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);
        bool close = agent.remainingDistance < agent.stoppingDistance + 0.1;
        if (close)
            move = Vector3.zero;
        animator.SetFloat("forward", move.z);
        animator.SetFloat("turn", move.x);
        animator.SetBool("attack", close && Random.value < 0.05f);
    }

    static void KillPart(Transform p, Vector3 force)
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
            r.AddForce(force);
            Destroy(smr);
            Destroy(g, Random.Range(5f, 15f));
        }
        else
            Destroy(g);

        int cnt = p.childCount;
        for (int i = 0; i < cnt; i++)
            KillPart(p.GetChild(i), force);
        p.transform.DetachChildren();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
            return;
        KillPart(transform, col.impulse / GetComponent<Rigidbody>().mass);
    }
}
