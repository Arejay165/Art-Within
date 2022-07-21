using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnvironmentalPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    public GameObject panel;
    public string message;
  


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
            panel.gameObject.SetActive(true);
            text.text = message;
      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panel.gameObject.SetActive(false);
            //  DialogueManager.instance.narratorDialoguePanel.SetActive(false);
            text.text = "";
        
        }
    }
}
