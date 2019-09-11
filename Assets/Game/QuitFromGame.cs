using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitFromGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Init"); // go back to menu
    }
}
