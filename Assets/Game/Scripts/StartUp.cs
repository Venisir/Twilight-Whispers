using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = true;
    }

    private void Start()
    {
        SetLanguage();
    }

    private void SetLanguage()
    {
        string userLanguage = Registry.UserLanguage;
        if (string.IsNullOrEmpty(userLanguage))
        {
            userLanguage = Application.systemLanguage.ToString();
        }
    }
}