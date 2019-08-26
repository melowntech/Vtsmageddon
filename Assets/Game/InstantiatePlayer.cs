using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiatePlayer : MonoBehaviour
{
    public GameObject colliderPrefab;

    void Start ()
    {
        if (!SelectCar.SelectedCarPrefab)
        {
            SceneManager.LoadScene("Init"); // go back to menu
            return;
        }

        GameObject car = Instantiate(SelectCar.SelectedCarPrefab);
        GameObject map = FindObjectOfType<VtsMap>().gameObject;
        car.AddComponent<VtsRigidBodyActivate>().map = map;
        car.AddComponent<Reset>();
        {
            VtsColliderProbe cp = car.AddComponent<VtsColliderProbe>();
            cp.mapObject = map;
            cp.collidersLod = 20;
            cp.collidersDistance = 100; // this is gonna be heavy
            cp.colliderPrefab = colliderPrefab;
        }
        {
            var cam = FindObjectOfType<FollowingCamera>();
            cam.target = car;
            cam.enabled = true;
        }
    }
}
