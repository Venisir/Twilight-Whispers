using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditsPanel;

    [SerializeField]
    private GameObject _menuPanel;

    void Start()
    {
        AudioManager.Instance.EnableMenuMusic();
        HideCredits();
    }

    public void StartGame()
    {
        AudioManager.Instance.EnableGameMusic();
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        _creditsPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void HideCredits()
    {
        _creditsPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
