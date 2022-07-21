using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    GameObject player;
    List<GameObject> keyObjects = new List<GameObject>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().OnKeyPickup += CreateImage;
        player.GetComponent<PlayerController>().OnKeyUsed += RemoveImage;
    }

    void CreateImage(Sprite sprite)
    {
        GameObject imgObject = new GameObject("testAAA");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);

        Image image = imgObject.AddComponent<Image>();
        image.sprite = sprite;
        imgObject.transform.SetParent(this.transform);
        keyObjects.Add(imgObject);
    }

    void RemoveImage(int num)
    {
        Debug.Log("loop " + num + " times");
        for (int i = 0; i < num; i++)
        {
            Debug.Log(i);
            GameObject removedObject = keyObjects[0];
            keyObjects.Remove(keyObjects[0]);
            GameObject.Destroy(removedObject);
        }
    }
}
