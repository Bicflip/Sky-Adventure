using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool ispaused;
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused == true)
                resumegame();
            else
                pausegame();
        }
    }

    public void pausegame()
    {
        ispaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void resumegame()
    {
        ispaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;       
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
