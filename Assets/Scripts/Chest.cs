using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Chest : MonoBehaviour
{
    [SerializeField] GameObject interactComp;
    [SerializeField] GameObject Loot;
    [SerializeField] int DropCount;
    [SerializeField] float DropForce;
    [SerializeField] Transform SpawnOffset;
    [SerializeField] bool painted;
    [SerializeField] GameObject ChestCover;
    [SerializeField] Vector2 DropRange;
    List<GameObject> SpawnedLoot = new List<GameObject>();
    Paintable paintComp;
    public UnityEvent OnObjectSpawned;
    bool opened = false;

    PlayerInteractUI playerInteractUI;

    Collider collider;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInteractUI = player.gameObject.GetComponent<PlayerInteractUI>();
        interactComp.GetComponent<InteractableObj>().OnInteract += OpenChest;
        paintComp = GetComponentInChildren<Paintable>();
        collider = gameObject.GetComponent<Collider>();
        InitializeLoot();
        paintComp.ApplyUI();
        paintComp.hasInteractable = true;
        paintComp.PaintFilled += () =>
        {
            interactComp.GetComponent<InteractableObj>().ApplyUI();
            painted = true; 
        };


    }

    //void OnInteract()
    //{
    //    if (!open)
    //    {
    //        OpenDoor.SetActive(true);
    //        ClosedDoor.SetActive(false);

    //        Loot.SetActive(true);
    //        Loot.GetComponent<Pickup>().Launch();
    //        open = true;
    //    }
    //}

    void InitializeLoot()
    {
        for (int i = 0; i < DropCount; i++)
        {
            GameObject drop = GameObject.Instantiate(Loot);
            //drop.transform.localScale = new Vector3(0.5f, 0.5f);
            drop.transform.SetParent(this.transform);
            drop.transform.localPosition = SpawnOffset.localPosition;
            drop.transform.rotation = transform.rotation;
            //var direction = transform.position - Camera.main.transform.position;
            //drop.transform.localRotation = Quaternion.LookRotation(direction);

            Pickup dropData = drop.GetComponent<Pickup>();
            int angle = Random.Range((int)DropRange.x, (int)DropRange.y);
            dropData.Init(DropForce, angle, true);

            SpawnedLoot.Add(drop);
            drop.SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            OpenChest();
        }
    }
    void OpenChest()
    {
        if (!opened && painted)
        {
            DropLoot();
            collider.enabled = false;
            ChestCover.transform.localRotation = Quaternion.Euler(0, 0, 0);
            playerInteractUI.DisableUI();
            OnObjectSpawned?.Invoke();
            painted = false;
            opened = true;
         
        }
    }
    IEnumerator EnemySpawnDelay()
    {
        yield return new WaitForSeconds(5f);
        OnObjectSpawned?.Invoke();
    }
    void DropLoot()
    {
        foreach (GameObject loot in SpawnedLoot)
        {
            loot.transform.SetParent(null);
            loot.SetActive(true);
            loot.GetComponent<Pickup>().Launch();
        }
    }
}
