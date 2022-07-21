using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] List<GameObject> HandPositions;
    [SerializeField] GameObject HourHand;
    [SerializeField] GameObject MinHand;
    [SerializeField] GameObject Light;

    public bool flipHand = false;
    public Action OnClockTimerEnd;

    public void MoveHandTo(GameObject num1, GameObject num2)
    {
        RotateZTowardsObject(HourHand, num1);
        RotateZTowardsObject(MinHand, num2);
    }

    void RotateZTowardsObject(GameObject rotObj, GameObject lookObj)
    {
        float AngleRad = Mathf.Atan2(lookObj.transform.position.y - rotObj.transform.position.y,
        lookObj.transform.position.x - rotObj.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        rotObj.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    IEnumerator TickClock(float seconds)
    {
        Light.SetActive(true);
        MinHand.transform.rotation = Quaternion.Euler(0, 0, GetAngleSeconds(seconds));

        float currAngle = GetAngleSeconds(seconds);
        float secondsReference = seconds;

        while (secondsReference > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            secondsReference--;
            if (flipHand)
            {
                currAngle += 6;
            }
            else
            {
                currAngle -= 6;
            }
            MinHand.transform.rotation = Quaternion.Euler(0, 0, currAngle);
        }
        Light.SetActive(false);
        OnClockTimerEnd?.Invoke();
    }

    float GetAngleSeconds(float seconds)
    {
        float angle = 6 * seconds;
        if (flipHand)
        {
            angle = 360 - angle;
        }
        return angle;
    }

    public void SetClockTimer(float seconds)
    {
        StopAllCoroutines();
        ResetHands();
        StartCoroutine(TickClock(seconds));
    }

    public void ResetHands()
    {
        StopAllCoroutines();
        Light.SetActive(false);
        HourHand.transform.rotation = Quaternion.Euler(0, 0, 90);
        MinHand.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
