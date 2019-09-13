using System.Collections.Generic;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    public UnityEngine.UI.Text textScore;
    public UnityEngine.UI.Text textRate;

    private static uint score = 0;
    private static int bestRate = 0;
    private static Queue<System.DateTime> timestamps = new Queue<System.DateTime>();

    void UpdateTimestamps()
    {
        // remove stalled timestamps
        var thr = System.DateTime.Now + System.TimeSpan.FromSeconds(-120);
        while (timestamps.Count > 0 && timestamps.Peek() < thr)
            timestamps.Dequeue();
    }

    void Update()
    {
        if (!textScore)
            return;
        if (Input.GetAxis("Restart") > 0)
        {
            score = 0;
            bestRate = 0;
            timestamps.Clear();
            return;
        }
        UpdateTimestamps();
        textScore.text = score.ToString();
            textRate.text = "- (" + bestRate.ToString() + ")";
        if (timestamps.Count >= 1)
        {
            float duration = (float)(System.DateTime.Now - timestamps.Peek()).TotalSeconds;
            if (duration > 30)
            {
                int rate = Mathf.RoundToInt(60f * timestamps.Count / duration);
                if (rate > bestRate)
                    bestRate = rate;
                textRate.text = rate.ToString() + " (" + bestRate.ToString() + ")";
            }
        }
    }

    public static void AddScore()
    {
        score++;
        timestamps.Enqueue(System.DateTime.Now);
    }
}
