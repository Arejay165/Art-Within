using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class EndingCutscene : MonoBehaviour
{
    [SerializeField] List<GameObject> Scenes;
    GameObject CurrentScene;

    [Header("Cutscene Properties")]
    [SerializeField] float FadeSpeed = 0.5f;
    [SerializeField] float SceneLength = 3f;
    [SerializeField] GameObject Fade;
    Coroutine Scene;

    int currentIndex = 0;

    public UnityEvent OnCutsceneEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement_CC>().Stop();
            CurrentScene = Scenes[currentIndex];
            StartCoroutine(PlayScene());
        }
    }

    public void DisplayNextSlide()
    {
        CurrentScene.GetComponent<CanvasGroup>().DOFade(0f, FadeSpeed);

        CurrentScene = Scenes[currentIndex++];

        CurrentScene.GetComponent<CanvasGroup>().DOFade(1f, FadeSpeed);
    }

    IEnumerator PlayScene()
    {
        //Initial Scene
        CurrentScene.GetComponent<CanvasGroup>().DOFade(1f, FadeSpeed);
        yield return new WaitForSeconds(SceneLength);
        currentIndex++;
        while (currentIndex < Scenes.Count)
        {
            DisplayNextSlide();
            yield return new WaitForSeconds(SceneLength);
        }

        OnCutsceneEnd?.Invoke();
        yield return new WaitForSeconds(8f);
        Fade.GetComponent<CanvasGroup>().DOFade(1f, FadeSpeed);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");
    }
}
