using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectionRadius : MonoBehaviour
{
    public Action<GameObject, GameObject> OnDetectionEnter;
    public Action<GameObject, GameObject> OnDetectionExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnDetectionEnter?.Invoke(this.gameObject, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDetectionExit?.Invoke(this.gameObject, other.gameObject);
        }
    }
}
