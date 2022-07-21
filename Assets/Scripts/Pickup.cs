using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [Header("Launch Param")]
    [SerializeField] float force;
    [SerializeField] float angle;
    [SerializeField] bool willJump;

    bool canPickUp = true;
    Rigidbody rb;
    protected AudioSource audioSource;
    FloatGameobjects floatComp;
    SpriteRenderer rend;

    bool insideTerrain;
   // [SerializeField] protected AudioClip clip;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        floatComp = GetComponent<FloatGameobjects>();
        rend = GetComponent<SpriteRenderer>();
    }
    public void Init(float f, float a, bool j)
    {
        force = f;
        angle = a;
        willJump = j;
    }
    public void Launch()
    {
        canPickUp = false;

        angle *= Mathf.Deg2Rad;
        float xComponent = Mathf.Cos(angle) * force;
        float zComponent = Mathf.Sin(angle) * force;
        float yForce = willJump ? force : 0;
        Vector3 forceApplied = new Vector3(xComponent, yForce, zComponent);

        rb.AddForce(forceApplied);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0)
        {
            insideTerrain = true;
            Debug.Log("In terrain: " + other.name);
            rb.velocity = Vector3.zero;
        }
        if (other.gameObject.layer == 6)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            canPickUp = true;
            floatComp.enabled = true;

            if (insideTerrain)
            {
                //Debug.Log("In terrain: " + gameObject.name);
            }

            // audioSource.PlayOneShot(clip);
        }
        if (other.CompareTag("Player") && canPickUp)
        {
            OnPickUp(other.gameObject);
            audioSource.Play();
            canPickUp = false;

            rend.enabled = false;
            Destroy(gameObject,0.6f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 0)
        {
            insideTerrain = false;
        }
    }

    protected abstract void OnPickUp(GameObject taker);
}
