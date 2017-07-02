using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {
    
	void Start ()
    {
        AudioManager.Instance.EnableMenuMusic();
    }
	
    public void StartGame()
    {
        AudioManager.Instance.EnableGameMusic();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
