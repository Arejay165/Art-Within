using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    private Tween fadeTween;
    public Movement_CC player;

    public void Fade(float endValue, float duration, TweenCallback OnEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvas.DOFade(endValue, duration);
        fadeTween.onComplete += OnEnd;
    }

    //Helper functions
    public void FadeInThenOut()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvas.DOFade(1, 1);
        fadeTween.onComplete += () => StartCoroutine(WaitTillFade());
    }

    IEnumerator WaitTillFade()
    {
        yield return new WaitForSeconds(3f);
        fadeTween = canvas.DOFade(0, 1);
        fadeTween.onComplete += () => player.canMove = true;
    }

    IEnumerator BackupTimer(float endValue, float time, TweenCallback OnEnd)
    {
        yield return new WaitForSeconds(time);
        fadeTween.Kill(false);
        fadeTween = canvas.DOFade(endValue, 0f);
        fadeTween.onComplete += OnEnd;
    }

    public void BlockRayCast(bool c)
    {
        canvas.interactable = c;
        canvas.blocksRaycasts = c;
    }
}
