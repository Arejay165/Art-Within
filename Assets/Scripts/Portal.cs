using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    public bool IsLevelTransition;
    public string NextLevel;
    public bool Automatic = true;
    bool isTransitioning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Automatic || isTransitioning)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().enabled = false;
            GoToNextLevel();
        }

    }

    public void GoToNextLevel()
    {
        if (isTransitioning)
            return;
        isTransitioning = true;
        if (IsLevelTransition)
        {
            GlobalDataHandler.CURRENTLEVEL++;
        }
        if (NextLevel != "")
        {
            TransitionManager.instance.TransitionToNextLevel(NextLevel);
        }
        else
        {
            TransitionManager.instance.TransitionToNextLevel();
        }
    }
}