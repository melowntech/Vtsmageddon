using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    public UnityEngine.UI.Text textScore;
    public UnityEngine.UI.Text textRate;

    private System.DateTime startTime;
    private int bestRate = 0;

    void Start()
    {
        startTime = System.DateTime.Now;
    }

    void Update()
    {
        if (Input.GetAxis("Restart") > 0)
        {
            ZombieController.killCount = 0;
            startTime = System.DateTime.Now;
        }
        textScore.text = ZombieController.killCount.ToString();
        double duration = (System.DateTime.Now - startTime).TotalSeconds;
        int rate = Mathf.RoundToInt((float)(60d * ZombieController.killCount / duration));
        if (ZombieController.killCount >= 10 && rate > bestRate)
            bestRate = rate;
        textRate.text = rate.ToString() + " (" + bestRate.ToString() + ")";
    }
}
