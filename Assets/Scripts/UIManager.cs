using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenu;
    public GameObject confirmationMenu;
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Lv0_Blocking - A");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateGameObjects(string nameOfGameObject)
    {
        if(mainMenu != null)
        mainMenu.SetActive(mainMenu.name.Equals(nameOfGameObject));
        confirmationMenu.SetActive(confirmationMenu.name.Equals(nameOfGameObject));
        if (pauseMenu != null)
            pauseMenu.SetActive(confirmationMenu.name.Equals(nameOfGameObject));
    }
}
