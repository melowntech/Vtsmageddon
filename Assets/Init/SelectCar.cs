using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCar : MonoBehaviour
{
    public GameObject CarPrefab;

    public static GameObject SelectedCarPrefab;

    public void OnClick()
    {
        SelectedCarPrefab = CarPrefab;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
