using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    private UnityEngine.UI.Text text;

    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        if (Input.GetAxis("Restart") > 0)
            ZombieController.killCount = 0;
        text.text = ZombieController.killCount.ToString();
    }
}
