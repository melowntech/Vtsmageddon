using UnityEngine;

public class DroneSensors : MonoBehaviour
{
    // degrees
    public float pitch = 0;
    public float pitchRate = 0;
    public float yaw = 0;
    public float yawRate = 0;
    public float roll = 0;
    public float rollRate = 0;
    public float altitude = 0;
    public float altitudeRate = 0;

    new private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    static void ProgressiveCorrection(ref float accumulated, float sensor)
    {
        // when the drone is upside-down, the euler angles converted from quaternion might have swapped axes
        //   therefore we only do the corrections when the readings and the accumulated values are close enough
        sensor = (sensor + 360) % 360;
        float a = accumulated;
        while (a < 0)
            a += 360;
        a = a % 360;
        float d = sensor - a;
        if (d < -180)
            d += 360;
        if (d > 180)
            d -= 360;
        if (Mathf.Abs(d) < 30)
            accumulated += d * 0.1f;
    }

    void ProgressiveCorrection()
    {
        // integrating angles from the rotation rates will slowly accumulate error that needs to be corrected
        Vector3 ea = transform.rotation.eulerAngles;
        ProgressiveCorrection(ref pitch, ea[0]);
        ProgressiveCorrection(ref roll, ea[2]);
        ProgressiveCorrection(ref yaw, ea[1]);
    }

    void FixedUpdate()
    {
        Vector3 ea = rigidbody.angularVelocity * 180 / Mathf.PI * Time.fixedDeltaTime;
        ea = Quaternion.Inverse(rigidbody.rotation) * ea;
        pitchRate = (ea.x + 180) % 360 - 180;
        yawRate = (ea.y + 180) % 360 - 180;
        rollRate = (ea.z + 180) % 360 - 180;
        pitch += pitchRate;
        yaw += yawRate;
        roll += rollRate;
        altitude = transform.position.y;
        altitudeRate = rigidbody.velocity.y;
        ProgressiveCorrection();
    }

    public void hardReset()
    {
        pitch = 0;
        yaw = 0;
        roll = 0;
        altitude = 0;
        pitchRate = 0;
        yawRate = 0;
        rollRate = 0;
        altitudeRate = 0;
    }
}
