using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public TextMeshProUGUI text;
   // [HideInInspector]
    public NPCAnimation nPCAnimation;

    public Image speakerIcon;
    public Image bustIcon;
    public GameObject personaPanel;
    public TextMeshProUGUI speakerName;
    public Animator emojiAnim;
    //public bool toBeDestroyed;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void DestroyAfter()
    {
        Destroy(gameObject);
    }
   
}
