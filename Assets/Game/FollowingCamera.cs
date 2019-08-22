using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject target;

    public Vector3 pivotOffset = new Vector3(0, 2, 0);
    public float minDistance = 5;
    public float maxDistance = 50;
    public float distance = 15;
    public float cameraSize = 3;
    public float minPitch = 0;
    public float maxPitch = 90;
    public float pitch = 15;
    public float yaw = 0;
    public float cameraMouseSensitivity = 8;

    private float initPitch;
    private float initYaw;
    private float initDistance;
    private float dst;

    private void Ranges()
    {
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private Vector3 Position()
    {
        Quaternion rot = Rotation();
        Vector3 pivot = target.transform.position + target.transform.rotation * pivotOffset;
        RaycastHit hit;
        dst = Mathf.Lerp(dst, distance, 0.1f);
        for (int y = -3; y < 4; y++)
        {
            for (int x = -3; x < 4; x++)
            {
                Vector3 dir = (rot * new Vector3(x * cameraSize, y * cameraSize, -maxDistance)).normalized;
                Debug.DrawRay(pivot, dir * distance);
                if (Physics.Raycast(pivot, dir, out hit, distance))
                    dst = Mathf.Min(hit.distance - 0.5f, dst);
            }
        }
        return pivot + rot * new Vector3(0, 0, -dst);
    }

    private Quaternion Rotation()
    {
        return target.transform.rotation * Quaternion.Euler(pitch, yaw, 0);
    }

    private void Start()
    {
        initPitch = pitch;
        initYaw = yaw;
        dst = initDistance = distance;
        Ranges();
        transform.rotation = Rotation();
        transform.position = Position();
    }

    void FixedUpdate()
    {
        Ranges();
        float interp = Cursor.lockState == CursorLockMode.Locked ? 0.3f : 0.1f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation(), interp);
        transform.position = Vector3.Lerp(transform.position, Position(), interp);
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += Input.GetAxis("Mouse X") * cameraMouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * cameraMouseSensitivity;
            distance *= Mathf.Pow(0.1f, Input.GetAxis("Mouse ScrollWheel"));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = Cursor.lockState == CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            pitch = initPitch;
            yaw = initYaw;
            distance = initDistance;
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
