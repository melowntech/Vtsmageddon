using UnityEngine;

public class ActivateMinimapMarker : MonoBehaviour
{
	void Update()
    {
        GetComponent<MeshRenderer>().enabled = true;
        Destroy(this);
	}
}
