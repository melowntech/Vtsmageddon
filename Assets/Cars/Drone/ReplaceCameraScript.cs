using UnityEngine;

public class ReplaceCameraScript : MonoBehaviour
{
    public GameObject objective;

    void Start()
    {
        GameObject go = FindObjectOfType<FollowingCamera>().gameObject;
        Destroy(go.GetComponent<FollowingCamera>());
        DroneCamera dc = go.AddComponent<DroneCamera>();
        dc.targetFirst = objective;
        dc.targetFollow = gameObject;
        Destroy(this);
    }
}
