using UnityEngine;

public class SelectMap : MonoBehaviour
{
    public string MapUrl;
    public Vector3 MapPosition;
    public Vector3 SunDirection;

    public static string SelectedUrl;
    public static Vector3 SelectedPosition;
    public static Vector3 SelectedSunDirection;

    public void OnClick()
    {
        SelectedUrl = MapUrl;
        SelectedPosition = MapPosition;
        SelectedSunDirection = SunDirection;
        if (SelectCar.SelectedCarPrefab != null)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
