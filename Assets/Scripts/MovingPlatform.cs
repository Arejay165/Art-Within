using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up,
    Down,
    Left,
    Right
};
public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;

    [SerializeField]
    private float speed;

    public Transform pointA, pointB;

    public Direction direction;

    public bool isOnNormalPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void FixedUpdate()
    {
        if (!isOnNormalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, speed * Time.fixedDeltaTime);
        }
        else if(isOnNormalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, speed * Time.fixedDeltaTime);
        }

        if(Vector3.Distance(transform.position, pointB.position) < 0.02f)
        {
            isOnNormalPos = true;
        }

        if(Vector3.Distance(transform.position, pointA.position) < 0.02f)
        {
            isOnNormalPos = false;
        }

        //if (transform.position == pointB.position)
        //{
        //    isOnNormalPos = true;
        //}else if(transform.position == pointA.position)
        //{
        //    isOnNormalPos = false;
        //}
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("out of moving platform");
            other.transform.SetParent(null);
        }
    }



    //public void PlatformDirection()
    //{
    //    switch (direction)
    //    {
    //        case Direction.Up:
    //            transform.Translate(Vector3.up * speed * Time.deltaTime);
    //            break;
    //        case Direction.Down:
    //            transform.Translate(Vector3.down * speed * Time.deltaTime);
    //            break;

    //        case Direction.Left:
    //            transform.Translate(Vector3.left * speed * Time.deltaTime);
    //            break;

    //        case Direction.Right:
    //            transform.Translate(Vector3.right * speed * Time.deltaTime);
    //            break;
    //    }
    //}
}
