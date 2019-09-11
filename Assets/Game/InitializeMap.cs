using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeMap : MonoBehaviour
{
    void Start()
    {
        if (string.IsNullOrEmpty(SelectMap.SelectedUrl))
            SceneManager.LoadScene("Init"); // go back to menu
    }

    void Update()
    {
        if (string.IsNullOrEmpty(SelectMap.SelectedUrl))
            return;
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
