using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    private NavMeshSurface surface;
    private AsyncOperation op;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void Update()
    {
        if (op != null && !op.isDone)
            return;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            surface.center = player.transform.position;
        op = surface.UpdateNavMesh(surface.navMeshData);
    }
}
