using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    public List<GameObject> Level1GameObjects;
    public List<GameObject> Level2GameObjects;
    public List<GameObject> PostLevel2GameObjects;
    public List<GameObject> PreEndingGameObjects;

    [SerializeField] int currentLevel;
    // Start is called before the first frame update
    void Awake()
    {
        currentLevel = GlobalDataHandler.CURRENTLEVEL;
        switch (GlobalDataHandler.CURRENTLEVEL)
        {
            case 1:
                EnableLevelObjects(Level1GameObjects);
                break;
            case 2:
                EnableLevelObjects(Level2GameObjects);
                break;
            case 3:
                EnableLevelObjects(PostLevel2GameObjects);
                break;
            case 4:
                EnableLevelObjects(PreEndingGameObjects);
                break;
            default:
                break;
        }

    }

    void EnableLevelObjects(List<GameObject> objects)
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(true);
        }
    }    

}
