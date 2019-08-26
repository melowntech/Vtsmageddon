using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public GameObject target;

    public float targetOffset;

    private Quaternion Rotation()
    {
        return Quaternion.Euler(90, target.transform.rotation.eulerAngles[1], 0);
    }

    private void Start()
    {
        transform.rotation = Rotation();
        transform.position = target.transform.position + new Vector3(0, targetOffset, 0);
    }

    void FixedUpdate()
    {
        Quaternion rot = Rotation();
        transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0, targetOffset, 0), 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);
    }
}
