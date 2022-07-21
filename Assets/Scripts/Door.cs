using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool paintable;
    [SerializeField] bool locked;

    InteractableObj interactComp;
    Paintable paintableComp;
    void Start()
    {
        interactComp = GetComponentInChildren<InteractableObj>();
        paintableComp = GetComponentInChildren<Paintable>();
        
        interactComp.OnInteract += OnInteract;
    }

    public void MakePaintable()
    {
        paintableComp.paintable = true;
        paintableComp.PaintFilled += () => locked = false;
    }

    void OnInteract()
    {
        if (!locked)
            gameObject.SetActive(false);
        else
            Debug.Log("it's not painted");
    }
}
