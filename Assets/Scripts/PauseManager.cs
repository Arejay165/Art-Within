using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : UIManager
{
    // Start is called before the first frame update
    bool isPause;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        confirmationMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        //consult kaell about restarting level
        //LevelManager.instance.
    }

    public void Pause()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(true);
        confirmationMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 0;
        
    }
}
