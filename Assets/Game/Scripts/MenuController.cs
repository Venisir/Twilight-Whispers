using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsPanel;

    [SerializeField]
    private GameObject _menuPanel;

    [SerializeField]
    private GameObject _loadingPanel;

    void Start()
    {
        AudioManager.Instance.EnableMenuMusic();
        HideCredits();
    }

    public void StartGame()
    {
        AudioManager.Instance.EnableGameMusic();
        _loadingPanel.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        _creditsPanel.SetActive(true);
        _menuPanel.SetActive(false);
        _loadingPanel.SetActive(false);
    }

    public void HideCredits()
    {
        _creditsPanel.SetActive(false);
        _menuPanel.SetActive(true);
        _loadingPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetLanguage(int i)
    {
        LocalizationManager.Instance.ChangeLanguage((GameData.Languages)i);
    }
}
