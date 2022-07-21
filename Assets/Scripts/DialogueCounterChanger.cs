using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DialogueCounterChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;

    private RuskatConvo ruskatConvo;

    GameObject ruskat;
    public bool destroyable;


    public UnityEvent onTrigger;

    [SerializeField]
    bool isImportant;

    private void Start()
    {
        ruskat = GameObject.FindGameObjectWithTag("Ruskat");
        ruskatConvo = ruskat.GetComponent<RuskatConvo>();
      
    }

    private void OnEnable()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ruskatConvo.dialogueIndex = index;
            onTrigger?.Invoke();
            if (destroyable)
            {
                Destroy(gameObject);
               // ruskatConvo.newTalk.SetActive(false);
            }

            if(isImportant)
            ruskatConvo.newTalk.SetActive(true);
        }

       
    }


    private void OnTriggerExit(Collider other)
    {
       // if(isImportant)
       // ruskatConvo.newTalk.SetActive(false);
    }

    private void OnDestroy()
    {
     //   if (isImportant)
          //  ruskatConvo.newTalk.SetActive(false);
    }
}
