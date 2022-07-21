using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class IntroCutscene : MonoBehaviour
{
    [SerializeField] List<GameObject> Scenes;
    GameObject CurrentScene;

    [Header("Cutscene Properties")]
    [SerializeField] float FadeSpeed = 0.5f;
    [SerializeField] float SceneLength = 3f;
    [SerializeField] GameObject Fade;
    [SerializeField] GameObject BGCover;
    [SerializeField] GameObject Buttons;
    Coroutine Scene;

    bool playing = false;

    int currentIndex = 0;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<Movement_CC>().Stop();
    //        CurrentScene = Scenes[currentIndex];
    //        StartCoroutine(PlayScene());
    //    }
    //}

    public void PlayIntroScene()
    {
        if (playing)
            return;

        playing = true;

        CurrentScene = Scenes[currentIndex];
        StartCoroutine(PlayScene());
        Buttons.SetActive(false);
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
        BGCover.GetComponent<CanvasGroup>().DOFade(1f, FadeSpeed);
        yield return new WaitForSeconds(SceneLength);
        currentIndex++;
        while (currentIndex < Scenes.Count)
        {
            DisplayNextSlide();
            yield return new WaitForSeconds(SceneLength);
        }

        yield return new WaitForSeconds(3f);
        Fade.GetComponent<CanvasGroup>().DOFade(1f, FadeSpeed);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Lv0_Blocking - A");
    }
}
