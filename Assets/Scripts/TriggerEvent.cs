using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : ReqCounter
{
    // Start is called before the first frame update

    public bool isDestroyable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("req com");
            RequirementComplete();
            if(isDestroyable)
            Destroy(gameObject);
        }
    }


}
