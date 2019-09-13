using UnityEngine;

public class DroneMotors : MonoBehaviour
{
    public float[] throttle = new float[4]; // FL, FR, RL, RR
    public float forcePerThrottle = 10;
    public float torquePerThrottle = 10;
    public float rotationsPerThrottle = 500;

    private float[] throttleAcc = new float[4]; // FL, FR, RL, RR
    private RotorSpin[] rotors = new RotorSpin[4];
    new private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rotors[0] = transform.Find("drone/rotor_fl").GetComponent<RotorSpin>();
        rotors[1] = transform.Find("drone/rotor_fr").GetComponent<RotorSpin>();
        rotors[2] = transform.Find("drone/rotor_rl").GetComponent<RotorSpin>();
        rotors[3] = transform.Find("drone/rotor_rr").GetComponent<RotorSpin>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
            throttle[i] = Mathf.Clamp01(throttle[i]);

        for (int i = 0; i < 4; i++)
        {
            int negator = i == 0 || i == 3 ? 1 : -1;
            throttleAcc[i] = throttleAcc[i] * 0.95f + throttle[i] * 0.05f;
            Vector3 force = transform.up * forcePerThrottle * throttleAcc[i];
            Vector3 offset = transform.position
                + transform.right * (i % 2 == 0 ? -1 : 1) * 0.28f
                + transform.forward * (i / 2 == 0 ? 1 : -1) * 0.28f;
            rigidbody.AddForceAtPosition(force, offset);
            rigidbody.AddTorque(transform.up * throttleAcc[i] * -negator * torquePerThrottle);
            rotors[i].Spin(throttleAcc[i] * negator * rotationsPerThrottle);
        }
    }

    public void hardReset()
    {
        for (int i = 0; i < 4; i++)
        {
            throttle[i] = 0;
            throttleAcc[i] = 0;
        }
    }
}
