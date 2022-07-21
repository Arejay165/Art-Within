using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatGameobjects : MonoBehaviour
{
    // Start is called before the first frame update

    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float offset = 0;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();


    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + offset;

        transform.position = tempPos;
    }


}
