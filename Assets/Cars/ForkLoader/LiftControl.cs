using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftControl : MonoBehaviour
{
    public Transform lift;
    public float min = 0;
    public float max = 1;
    public float speed = 0.01f;

    void FixedUpdate()
    {
        float change = 0;
        if (Input.GetKey(KeyCode.R))
            change = 1;
        else if (Input.GetKey(KeyCode.F))
            change = -1;

        float y = lift.localPosition.y;
        y += change * speed;
        y = Mathf.Clamp(y, min, max);
        lift.localPosition = new Vector3(0, y, lift.localPosition.z);
    }
}
