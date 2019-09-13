using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public uint zombiesLimit = 30;
    private GameObject player;
    public static Vector3 zombiesTarget = new Vector3();

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        // spawn more zombies
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < zombiesLimit)
        {
            float dist = Random.Range(50f, 90f);
            float ang = Random.Range(0, Mathf.PI * 2);
            Vector3 origin = player.transform.position + new Vector3(Mathf.Cos(ang) * dist, 1000, Mathf.Sin(ang) * dist);
            RaycastHit hit;
            if (Physics.Raycast(origin, new Vector3(0, -1, 0), out hit, Mathf.Infinity, 1 << 30))
                Instantiate(zombiePrefab, hit.point, Quaternion.identity, transform);
        }

        { // zombies target
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, 1 << 30))
                zombiesTarget = hit.point;
        }
    }
}
