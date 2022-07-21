using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    public List<Sprite> scenes;
    int currentIndex = 0;

    public Image imageUI;
    public GameObject canvas;
    [SerializeField] AIConvo aIConvo;
    public UnityEvent OnCutSceneStart;
    public UnityEvent OnCutSceneEnd;

    Prompt prompt;
    InteractableObj interact;
    PlayerController player;

   


    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        interact = GetComponentInChildren<InteractableObj>();

        prompt = GetComponentInParent<Prompt>();
        
        prompt.InteractAction += StartCutscene;

        aIConvo.changeIndex += ChangeCutsceneImage;
    }

  

    public void StartCutscene()
    {

        Debug.Log("Start Cutscene");
        OnCutSceneStart?.Invoke();
        canvas.SetActive(true);
        imageUI.gameObject.SetActive(true);
        player.enabled = false;
        GoToNextScene();
    }

    void GoToNextScene()
    {
        if (currentIndex == scenes.Count)
        {
            OnCutSceneEnd?.Invoke();
            imageUI.gameObject.SetActive(false);
            canvas.SetActive(false);
            player.enabled = true;
            return;
        }

        imageUI.sprite = scenes[currentIndex];
        currentIndex++;
    }

    public void ChangeCutsceneImage(int index)
    {
 

        imageUI.sprite = scenes[index];
    }

    private void OnDisable()
    {
        OnCutSceneEnd?.Invoke();
        imageUI.gameObject.SetActive(false);
        canvas.SetActive(false);
        if (player)
            player.enabled = true;
      //  return;
    }

    private void OnDestroy()
    {
        OnCutSceneEnd?.Invoke();
        imageUI.gameObject.SetActive(false);
        canvas.SetActive(false);
        if (player)
            player.enabled = true;
    }
}
