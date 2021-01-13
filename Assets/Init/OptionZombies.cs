using UnityEngine;
using UnityEngine.UI;

public class OptionZombies : MonoBehaviour
{
    public static bool ZombiesEnabled = true;

    private Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = ZombiesEnabled;
    }

    public void OnToggle()
    {
        ZombiesEnabled = toggle.isOn;
    }
}
