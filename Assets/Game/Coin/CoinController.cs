using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
    }

    void Update()
    {
        if ((transform.position - player.transform.position).magnitude > 100)
            Destroy(gameObject); // destroy this coin
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, 1);
    }
}
