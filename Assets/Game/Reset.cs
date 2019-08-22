using UnityEngine;

public class Reset : MonoBehaviour
{
    public float unstuckSpeed = 0.1f;

    private Vector3 initPos;
    private Quaternion initRot;

    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    void Stop()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetAxis("Unstuck") > 0)
        {
            Stop();
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles[1], 0);
            transform.position += transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 1, Input.GetAxis("Vertical")) * unstuckSpeed;
        }
        else if (Input.GetAxis("Restart") > 0)
        {
            transform.position = initPos;
            transform.rotation = initRot;
            Stop();
            var a = gameObject.AddComponent<VtsRigidBodyActivate>();
            a.map = FindObjectOfType<VtsMap>().gameObject;
        }
    }
}
