using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    private NavMeshSurface surface;
    private AsyncOperation op;
    private GameObject player;

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
        surface.center = player.transform.position;
        op = surface.UpdateNavMesh(surface.navMeshData);
    }
}
