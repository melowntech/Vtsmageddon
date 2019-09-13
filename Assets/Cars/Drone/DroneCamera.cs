using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    public GameObject targetFollow;
    public GameObject targetFirst;
    public bool firstPerson = false;

    private float y;

    void FixedUpdate()
    {
        float t = targetFollow.transform.rotation.eulerAngles.y;
        y = transform.rotation.eulerAngles.y;
        float d = t - y;
        if (d > 180)
            d = d - 360;
        if (d < -180)
            d = 360 + d;
        y += d * 0.1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            firstPerson = !firstPerson;

        if (firstPerson)
        {
            transform.rotation = targetFirst.transform.rotation;
            transform.position = targetFirst.transform.position;
        }
        else
        {
            transform.rotation = Quaternion.Euler(25, y, 0);
            transform.position = targetFollow.transform.position + transform.rotation * new Vector3(0, 0, -5);
        }
    }
}
