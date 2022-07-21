using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static LevelManager instance;
    PlayerController playerController;
    public string nextLevel;
    public string currentLevel;
  

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        return;

        // playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
     
    }

    private void OnEnable()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void TransferPlayerLocation(Transform loc)
    {
        playerController.gameObject.transform.position =  new Vector3(loc.position.x, loc.position.y, loc.position.z);
    }
}