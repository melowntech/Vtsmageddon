using System.Collections.Generic;
using UnityEngine;

public class CoinBreakUp : MonoBehaviour
{
    public List<GameObject> parts;

    private bool broken = false;

    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        if (broken)
            return;
        broken = true;
        Destroy(gameObject);
        foreach (GameObject p in parts)
        {
            GameObject g = Instantiate(p, transform.position, transform.rotation, null);
            Destroy(g, Random.Range(5f, 15f));
        }
        ScoreLabel.AddScore();
    }
}
