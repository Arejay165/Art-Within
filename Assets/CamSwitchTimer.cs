using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTimer : MonoBehaviour
{
    [SerializeField] float timerValue;
    bool running = false;
    public CinemachineSwitcher cam;

    public void StartTimer()
    {
        if (running)
            return;

        running = true;
        cam.player.GetComponent<Movement_CC>().Stop();
        cam.player.GetComponent<Movement_CC>().canMove = false;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timerValue);
        cam.ForceSwitch();
        cam.player.GetComponent<Movement_CC>().canMove = true;
        gameObject.SetActive(false);
    }
}
