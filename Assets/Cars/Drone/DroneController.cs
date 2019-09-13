using UnityEngine;

public class DroneController : MonoBehaviour
{
    public bool acrobaticMode = false;
    public bool armed = false;
    public float axisPitch = 0;
    public float axisRoll = 0;
    public float axisYaw = 0;
    public float axisThrottle = 0;

    protected PID pitchRatePid;
    protected PID rollRatePid;
    protected PID yawRatePid;
    protected float throttle = 0;

    protected PID pitchStabPid;
    protected PID rollStabPid;
    protected PID yawStabPid;
    protected float yaw = 0;
    protected PID altitudeStabPid;
    protected float altitude = 0;

    private DroneMotors dm;
    private DroneSensors ds;
    private Light diodeRed;
    private Light diodeYellow;
    private Light diodeGreen;

    void Start()
    {
        pitchRatePid = new PID(0.02f, 0.0001f, 0.2f);
        rollRatePid = new PID(0.02f, 0.0001f, 0.2f);
        yawRatePid = new PID(0.02f, 0.0001f, 0.2f);

        pitchStabPid = new PID(0.05f, 0.00005f, 0.2f);
        rollStabPid = new PID(0.05f, 0.00005f, 0.2f);
        yawStabPid = new PID(0.05f, 0.0002f, 0.2f);
        altitudeStabPid = new PID(0.05f, 0.0003f, 5);

        dm = GetComponent<DroneMotors>();
        ds = GetComponent<DroneSensors>();
        diodeRed = transform.Find("diode_red").GetComponent<Light>();
        diodeYellow = transform.Find("diode_yellow").GetComponent<Light>();
        diodeGreen = transform.Find("diode_green").GetComponent<Light>();
    }

    void FixedUpdate()
    {
        diodeRed.enabled = !armed;
        diodeYellow.enabled = armed && acrobaticMode;
        diodeGreen.enabled = armed && !acrobaticMode;

        if (!armed)
        {
            reset();
            return;
        }

        float pitchInput = axisPitch;
        float rollInput = axisRoll;
        float yawInput = axisYaw;
        float throttleInput = axisThrottle;

        if (acrobaticMode)
        {
            pitchInput *= 1.5f;
            rollInput *= 1.5f;
            yawInput *= 1.5f;
            throttle += throttleInput * 0.01f;
            pitchStabPid.reset();
            rollStabPid.reset();
            yawStabPid.reset();
            altitudeStabPid.reset();
            yaw = ds.yaw;
            altitude = ds.altitude;
        }
        else
        {
            yaw += yawInput * 1.5f;
            altitude += throttleInput * 0.1f;
            pitchInput = pitchStabPid.update(pitchInput * 45, ds.pitch);
            rollInput = rollStabPid.update(rollInput * 45, ds.roll);
            yawInput = yawStabPid.update(yaw, ds.yaw);
            throttle = altitudeStabPid.update(altitude, ds.altitude);
        }

        float pitchResponse = pitchRatePid.update(pitchInput, ds.pitchRate);
        float rollResponse = rollRatePid.update(rollInput, ds.rollRate);
        float yawResponse = yawRatePid.update(yawInput, ds.yawRate);

        throttle = Mathf.Clamp01(throttle);
        dm.throttle[0] = throttle - rollResponse - pitchResponse - yawResponse;
        dm.throttle[1] = throttle + rollResponse - pitchResponse + yawResponse;
        dm.throttle[2] = throttle - rollResponse + pitchResponse + yawResponse;
        dm.throttle[3] = throttle + rollResponse + pitchResponse - yawResponse;
    }

    public void reset()
    {
        pitchRatePid.reset();
        rollRatePid.reset();
        yawRatePid.reset();
        pitchStabPid.reset();
        rollStabPid.reset();
        yawStabPid.reset();
        altitudeStabPid.reset();
        yaw = ds.yaw;
        altitude = ds.altitude;
        throttle = 0;
        for (int i = 0; i < 4; i++)
            dm.throttle[i] = 0;
    }
}

// taken from https://forum.unity.com/threads/pid-controller.68390/
// and modified
[System.Serializable]
public class PID
{
    public float pFactor, iFactor, dFactor;

    private float integral;
    private float lastError;

    public PID(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
        reset();
    }

    public float update(float setpoint, float actual)
    {
        float present = setpoint - actual;
        integral += present;
        float deriv = present - lastError;
        lastError = present;
        return present * pFactor + integral * iFactor + deriv * dFactor;
    }

    public void reset()
    {
        integral = 0;
        lastError = 0;
    }
}
