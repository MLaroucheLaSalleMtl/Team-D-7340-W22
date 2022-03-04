using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class In_GameMenu : MonoBehaviour
{
    public static bool GameISPaused = false;

    public GameObject in_gameMeuuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameISPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        in_gameMeuuUI.SetActive(false);
        Time.timeScale = 1f;
        GameISPaused = false;
    }
    void Pause()
    {
        in_gameMeuuUI.SetActive(true);
        Time.timeScale = 0f;
        GameISPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Option()
    {
        SceneManager.LoadScene("Option");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

}
