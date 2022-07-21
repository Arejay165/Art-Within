using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Locker : MonoBehaviour
{
    [SerializeField] GameObject interactComp;
    [SerializeField] GameObject Loot;
    [SerializeField] int DropCount;
    [SerializeField] float DropForce;
    [SerializeField] bool painted;
    [SerializeField] GameObject LockerDoor;
    [SerializeField] Vector2 DropRange;
    List<GameObject> SpawnedLoot = new List<GameObject>();
    Paintable paintComp;
    public UnityEvent OnObjectSpawned;
    bool opened = false;
    void Start()
    {
        interactComp.GetComponent<InteractableObj>().OnInteract += OpenChest;
        paintComp = GetComponentInChildren<Paintable>();
        InitializeLoot();

        if(paintComp != null)
        {
            paintComp.ApplyUI();
            paintComp.hasInteractable = true;
            paintComp.PaintFilled += () =>
            {
                interactComp.GetComponent<InteractableObj>().ApplyUI();
                painted = true;
            };
        }
       
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
            drop.transform.localPosition = new Vector3(0, 2.5f, 0);
            Pickup dropData = drop.GetComponent<Pickup>();
            int angle = Random.Range((int)DropRange.x, (int)DropRange.y);
            //int angle = Random.Range(270, 270);
            dropData.Init(DropForce, angle, true);

            SpawnedLoot.Add(drop);
            drop.SetActive(false);
        }
    }
    void OpenChest()
    {
        if (!opened && painted)
        {
            DropLoot();

            LockerDoor.transform.localRotation = Quaternion.Euler(0, 100, 0);
            StartCoroutine(EnemySpawnDelay());
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
