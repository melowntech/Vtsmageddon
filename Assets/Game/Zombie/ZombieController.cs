using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private NavMeshAgent agent;
    private new Rigidbody rigidbody;
    private uint pathUpdateDelay = 0;
    private uint standingCounter = 0;
    private float targetDistance = 0;
    private bool targetDistanceHysteresis = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        //agent.updateRotation = false;
        agent.updateUpAxis = false;
        rigidbody = GetComponent<Rigidbody>();
        pathUpdateDelay = (uint)Random.Range(0, 1000);
        targetDistance = Mathf.Pow(Random.value, 0.5f) * 20;
    }

    void Update()
    {
        if (!agent.isOnNavMesh || (transform.position - player.transform.position).magnitude > 100)
        {
            Destroy(gameObject); // destroy this zombie
            return;
        }

        agent.nextPosition = transform.position;
        if (pathUpdateDelay++ % 50 == 13)
            agent.SetDestination(ZombieManager.zombiesTarget);
        if (!agent.hasPath)
            return;

        { // update base offset
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2, 1 << 30))
                agent.baseOffset = 1 - hit.point.y;
        }

        Vector3 move = agent.desiredVelocity;
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);

        if (agent.remainingDistance < targetDistance - 1)
            targetDistanceHysteresis = true;
        else if (agent.remainingDistance > targetDistance + 1)
            targetDistanceHysteresis = false;
        if (targetDistanceHysteresis)
            move = Vector3.zero;

        float spd = move.magnitude;
        if (move.magnitude > 1)
            move.Normalize();

        if (move.magnitude < 0.01f)
            standingCounter++;
        else
            standingCounter = 0;

        animator.speed = Mathf.Max(1, spd);
        animator.SetFloat("forward", move.z);
        animator.SetFloat("turn", move.x - 0.5f);
        if (standingCounter > 60)
        {
            if (Random.value < 0.003f)
                animator.SetBool("attack", true);
            else if (Random.value < 0.001f)
                animator.SetBool("hurt", true);
        }
        else
        {
            animator.SetBool("attack", false);
            animator.SetBool("hurt", false);
        }

        if (standingCounter > 600 && Random.value < 0.0001f)
            ZombieBreakUp.BreakUp(transform);
    }

    void OnAnimatorMove()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        Vector3 animatorVelocity = animator.deltaPosition / Time.deltaTime;
        animatorVelocity.y = currentVelocity.y;
        rigidbody.velocity = animatorVelocity;
    }
}
