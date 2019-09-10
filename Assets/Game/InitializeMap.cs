using UnityEngine;

public class InitializeMap : MonoBehaviour
{
    void Update()
    {
        VtsMap map = GetComponent<VtsMap>();
        map.Map.SetMapconfigPath(SelectMap.SelectedUrl);
        VtsMapMakeLocal mml = gameObject.AddComponent<VtsMapMakeLocal>();
        mml.x = SelectMap.SelectedPosition.x;
        mml.y = SelectMap.SelectedPosition.y;
        mml.z = SelectMap.SelectedPosition.z;
        mml.singleUse = true;
        GameObject sun = GameObject.Find("Sun");
        sun.transform.rotation = Quaternion.Euler(SelectMap.SelectedSunDirection);
        Destroy(this);
    }
}
