using System.Collections.Generic;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    public UnityEngine.UI.Text textScore;
    public UnityEngine.UI.Text textRate;

    public static uint killCount = 0;
    private int lastZombies = 0;
    private int bestRate = 0;
    private Queue<System.DateTime> zombies = new Queue<System.DateTime>();

    void CountZombies()
    {
        // remove stalled zombies
        {
            var thr = System.DateTime.Now + System.TimeSpan.FromSeconds(-120);
            while (zombies.Count > 0 && zombies.Peek() < thr)
                zombies.Dequeue();
        }

        // add new zombies
        {
            var t = System.DateTime.Now;
            while (lastZombies < killCount)
            {
                lastZombies++;
                zombies.Enqueue(t);
            }
        }
    }

    void Update()
    {
        if (!textScore)
            return;
        if (Input.GetAxis("Restart") > 0)
        {
            killCount = 0;
            lastZombies = 0;
            bestRate = 0;
            zombies.Clear();
            return;
        }
        CountZombies();
        textScore.text = killCount.ToString();
            textRate.text = "- (" + bestRate.ToString() + ")";
        if (zombies.Count >= 1)
        {
            float duration = (float)(System.DateTime.Now - zombies.Peek()).TotalSeconds;
            if (duration > 30)
            {
                int rate = Mathf.RoundToInt(60f * zombies.Count / duration);
                if (rate > bestRate)
                    bestRate = rate;
                textRate.text = rate.ToString() + " (" + bestRate.ToString() + ")";
            }
        }
    }
}
