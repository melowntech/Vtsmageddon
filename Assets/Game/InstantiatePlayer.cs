using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    public GameObject colliderPrefab;

    void Start()
    {
        GameObject car = Instantiate(SelectCar.SelectedCarPrefab, Vector3.zero, Quaternion.identity);
        GameObject map = FindObjectOfType<VtsMap>().gameObject;
        car.AddComponent<VtsRigidBodyActivate>().map = map;
        car.AddComponent<Reset>();
        {
            VtsColliderProbe cp = car.AddComponent<VtsColliderProbe>();
            cp.mapObject = map;
            cp.collidersLod = 20;
            cp.collidersDistance = 100;
            cp.colliderPrefab = colliderPrefab;
        }
        {
            var cam = FindObjectOfType<FollowingCamera>();
            cam.target = car;
            cam.enabled = true;
        }
        {
            var cam = FindObjectOfType<MinimapCamera>();
            cam.target = car;
            cam.enabled = true;
        }
    }
}
