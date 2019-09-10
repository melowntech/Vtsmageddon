using UnityEngine;

public class SelectCar : MonoBehaviour
{
    public GameObject CarPrefab;

    public static GameObject SelectedCarPrefab;

    public void OnClick()
    {
        SelectedCarPrefab = CarPrefab;
        if (SelectMap.SelectedUrl != null)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
