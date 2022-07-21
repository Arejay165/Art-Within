using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHopperUI : MonoBehaviour
{
    bool sceneLoading = false;
    public void GoToLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Update()
    {
        if (sceneLoading)
            return;
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Lv0_Blocking - A");
            sceneLoading = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Home");
            sceneLoading = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("WK13-School-Arjay");
            sceneLoading = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Lv2_W13_Kaell");
            sceneLoading = true;
        }
    }
}
