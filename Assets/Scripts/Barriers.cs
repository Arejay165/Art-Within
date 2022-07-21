using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barriers : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int keysNeeded;

    PlayerController playerController;

    public UnityEvent executeWhenDestroy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Stuff");
    //        playerController = collision.gameObject.GetComponent<PlayerController>();

    //        if(playerController.keys >= keysNeeded)
    //        {
    //            LevelManager.instance.ResetValueKeys(); // why???
    //            playerController.UseKey(keysNeeded);
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController.keys >= keysNeeded)
            {
                Debug.Log("Open");
              //  LevelManager.instance.ResetValueKeys();
                playerController.UseKey(keysNeeded);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        executeWhenDestroy?.Invoke();
    }


    private void OnDisable()
    {
        executeWhenDestroy?.Invoke();
    }

    
}
