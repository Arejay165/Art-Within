using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReqCounter : MonoBehaviour
{
    public int RequirementsCount;
    protected int RequirementsDone = 0;
    public UnityEvent OnRequirementsComplete;
    public List<GameObject> Requirements;
   
    public void RemoveFromReqList(GameObject obj)
    {
        if (Requirements.Contains(obj))
        {
            Requirements.Remove(obj);

        }
        if (Requirements.Count == 0)
        {
            Debug.Log(Requirements.Count);
            OnRequirementsComplete?.Invoke();
        }
    }
    public void RequirementComplete()
    {

        RequirementsDone++;
        if (RequirementsDone >= RequirementsCount)
        {
            OnRequirementsComplete?.Invoke();
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void StartWait(float time)
    {
        StartCoroutine(Wait(time));
    }
}
