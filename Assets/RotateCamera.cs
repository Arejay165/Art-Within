using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float minYValue = 0;
    public float maxYValue = 0;

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.rotation = Quaternion.Euler(0, Mathf.Clamp(LookAtRotation.eulerAngles.y, minYValue, maxYValue), 0);

        player.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
