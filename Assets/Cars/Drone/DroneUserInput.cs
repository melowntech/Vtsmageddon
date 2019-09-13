using UnityEngine;

public class DroneUserInput : MonoBehaviour
{
    private DroneSensors ds;
    private DroneMotors dm;
    private DroneController dc;
    new private Rigidbody rigidbody;

    void Start()
    {
        ds = GetComponent<DroneSensors>();
        dm = GetComponent<DroneMotors>();
        dc = GetComponent<DroneController>();
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Armed"))
            dc.armed = !dc.armed;

        if (Input.GetButtonDown("Acrobatic"))
            dc.acrobaticMode = !dc.acrobaticMode;

        if (Input.GetButtonDown("Restart"))
        {
            transform.position = Vector3.zero;
        }

        if (Input.GetButtonDown("Unstuck") || Input.GetButtonDown("Restart"))
        {
            transform.rotation = Quaternion.identity;
            rigidbody.rotation = Quaternion.identity;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            ds.hardReset();
            dm.hardReset();
            dc.reset();
            dc.armed = false;
        }

        if (!dc.armed)
        {
            dc.axisPitch = 0;
            dc.axisYaw = 0;
            dc.axisRoll = 0;
            dc.axisThrottle = 0;
            return;
        }

        if (dc.acrobaticMode)
        {
            dc.axisPitch = Input.GetAxis("AcroPitch");
            dc.axisRoll = Input.GetAxis("AcroRoll");
            dc.axisYaw = Input.GetAxis("AcroYaw");
            dc.axisThrottle = Input.GetAxis("AcroThrottle");
        }
        else
        {
            dc.axisPitch = Input.GetAxis("Pitch");
            dc.axisRoll = Input.GetAxis("Roll");
            dc.axisYaw = Input.GetAxis("Yaw");
            dc.axisThrottle = Input.GetAxis("Altitude");
        }
    }
}
