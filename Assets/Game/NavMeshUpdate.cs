using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    private NavMeshSurface surface;
    private GameObject player;
    private AsyncOperation op;
    private uint counter = 0;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        if (op != null && !op.isDone)
            return;
        op = null;
        if (counter++ % 120 != 13)
            return;
        surface.center = player.transform.position;
        op = surface.UpdateNavMesh(surface.navMeshData);
    }
}
