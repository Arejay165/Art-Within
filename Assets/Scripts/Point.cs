using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
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
        if (other.gameObject.GetComponent<MovingPlatform>())
        {
            //if(other.gameObject.GetComponent<MovingPlatform>().direction != null)
            //ChangePlatformDirection(other.gameObject.GetComponent<MovingPlatform>());
        }
        else
        {
            Debug.Log("Not Platform");
        }
    }

    void ChangePlatformDirection(MovingPlatform platform)
    {
        switch (platform.direction)
        {
            case Direction.Up:
                platform.direction = Direction.Down;
                break;
            case Direction.Down:
                platform.direction = Direction.Up;
                break;

            case Direction.Left:
                platform.direction = Direction.Right;
                break;

            case Direction.Right:
                platform.direction = Direction.Left;
                break;

        }
    }
}
