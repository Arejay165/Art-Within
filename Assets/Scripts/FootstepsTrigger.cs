using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip footstepsClip;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide player");
            other.gameObject.GetComponent<PlayerController>().walkingSource.clip = footstepsClip;
        }
    }
}
