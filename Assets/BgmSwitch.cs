using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmSwitch : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = clip;
            audioSource.Play();

            gameObject.SetActive(false);
        }
    }
}
