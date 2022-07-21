using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
   // public static CinemachineSwitcher instance;
   [SerializeField]
    bool isUsingOriginalCamera = true;

    private Animator animator;
    [SerializeField]private CinemachineVirtualCamera vcam;//playercam
    [SerializeField]private CinemachineVirtualCamera vcam2;//wallcam
    [SerializeField] Vector3 PlayerRotation;
    Vector3 OriginalRotation;

    public UnityEvent onTrigger;
    [HideInInspector] public GameObject player;
    private void Start()
    {
      //  if (instance == null)
        //    instance = this;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    //public void SwitchState()
    //{
    //    if (playerCam)
    //    {
    //        animator.Play("WallCamera");
    //    }
    //    else
    //    {
    //        animator.Play("PlayerCamera");
    //    }
    //    playerCam = !playerCam;
    //}

    public void SwitchPriority(GameObject player)
    {
        if (isUsingOriginalCamera)
        {
            vcam.Priority = 0;
            vcam2.Priority = 1;

            player.transform.rotation = Quaternion.Euler(PlayerRotation);
        }
        else
        {
            vcam.Priority = 1;
            vcam2.Priority = 0;

            player.transform.rotation = Quaternion.Euler(OriginalRotation);
        }
        isUsingOriginalCamera = !isUsingOriginalCamera;
    }

    public void ForceSwitch()
    {
        if (player)
            SwitchPriority(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OriginalRotation = other.transform.rotation.eulerAngles;
            SwitchPriority(other.gameObject);
            player = other.gameObject;
            onTrigger?.Invoke();
            //other.GetComponent<Movement_CC>().MovementToggle();
            //StartCoroutine(BufferToggle(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchPriority(other.gameObject);
            //StartCoroutine(BufferToggle(other.gameObject));
        }
    }

    //IEnumerator BufferToggle(GameObject other)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    other.GetComponent<Movement_CC>().MovementToggle();
    //}
}
