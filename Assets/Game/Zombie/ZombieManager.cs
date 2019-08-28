using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public uint zombiesLimit = 10;
    private GameObject player;

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < zombiesLimit)
        {
            float dist = Random.Range(50f, 90f);
            float ang = Random.Range(0, Mathf.PI * 2);
            Vector3 origin = player.transform.position + new Vector3(Mathf.Cos(ang) * dist, 1000, Mathf.Sin(ang) * dist);
            RaycastHit hit;
            if (Physics.Raycast(origin, new Vector3(0, -1, 0), out hit))
                Instantiate(zombiePrefab, hit.point, Quaternion.identity, null);
        }
    }
}
