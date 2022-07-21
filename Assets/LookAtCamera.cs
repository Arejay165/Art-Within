using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    GameObject cam;
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null)
            return;
        var direction = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
