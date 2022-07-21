
using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;


public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public RectTransform shutter;

    GameObject player;
    Animator UIAnimator;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        UIAnimator = player.GetComponent<PlayerInteractUI>().UITransition.GetComponent<Animator>();
        return;

    }

    public IEnumerator ShutterTransition(Action afterAction)
    {
        //shutter.gameObject.SetActive(true);
        //shutter.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 1500, 0);

        //shutter.DOAnchorPos3D(new Vector3(shutter.anchoredPosition3D.x, 0, shutter.anchoredPosition3D.z), 1f, false);

        UIAnimator.SetTrigger("PlayAnimation");

        yield return new WaitForSeconds(1.5f);
        afterAction.Invoke();
        yield return new WaitForSeconds(0.5f);
    }

  

    public void TransitionToNextLevel()
    {
        StartCoroutine(ShutterTransition(LevelManager.instance.NextLevel));
    }

    public void TransitionToNextLevel(string level)
    {
        LevelManager.instance.nextLevel = level;
        StartCoroutine(ShutterTransition(LevelManager.instance.NextLevel));
    }
    //public void MoveShutter()
    //{
    //    StartCoroutine(ShutterUp(null));
    //}


    //void DisableShutter(GameObject obj)
    //{
    //    obj.SetActive(false);
    //}

    //public void MoveShutter(GameObject obj)
    //{

    //}



}
