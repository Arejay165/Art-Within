using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class Gear : MonoBehaviour
{
    [SerializeField] float RotationSpeed;
    [SerializeField] Transform stopPoint;
    Vector3 originalPosition;
    bool inOriginalPoint = true;

    public bool Rotating = false;
    public bool Jammed = false;

    [Header("Materials")]
    Material OriginalMat;
    public Material notSpinningMat;
    public Material spinningMat;
    MeshRenderer meshRenderer;

    bool InitRotationState = false;
    public bool OriginallyRotating { get { return InitRotationState; } }
    Vector3 InitPos;
    Quaternion InitRot;
    public List<GameObject> ConnectedGears = new List<GameObject>();

    public Action OnGearJammed;
    public Action OnGearTurn;

    public bool powered = false;
    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        OriginalMat = meshRenderer.material;
        originalPosition = transform.position;
        InitRotationState = Rotating;
        InitPos = transform.position;
        InitRot = transform.rotation;
        StartCoroutine(InitializeGears());

        //if (stopPoint)
        //{
        //    stopPoint.transform.position = new Vector3(stopPoint.transform.position.x,
        //        stopPoint.transform.position.y, transform.position.z);
        //}
    }

    public void SetGlowingMaterial()
    {
        if (!powered)
            return; 

        if (Rotating && spinningMat)
        {
            meshRenderer.material = spinningMat;
            return;
        }

        if (notSpinningMat)
            meshRenderer.material = notSpinningMat;

    }

    public void ResetGear()
    {
        StopAllCoroutines();
        Rotating = InitRotationState;
        transform.position = InitPos;
        transform.rotation = InitRot;
        inOriginalPoint = true;
        SetGlowingMaterial();

        StartCoroutine(InitializeGears());
    }

    IEnumerator InitializeGears()
    {
        yield return new WaitForEndOfFrame();

        CheckGears(); 
        Rotating = InitRotationState;
    }
    private void Update()
    {
        if (!powered)
            return;

        if (Rotating)
        {
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }

    }

    public void Rotate(float otherRotation)
    {
        if (Rotating)
            return;

        RotationSpeed = otherRotation;
        

        foreach (GameObject gear in ConnectedGears)
        {
            Gear gearC = gear.GetComponent<Gear>();
            if (!gearC.Jammed)
            {
                Rotating = true;
                gearC.Rotate(RotationSpeed * -1);
                gearC.SetGlowingMaterial();
                OnGearTurn?.Invoke();
            }
            else
            {
                OnGearJammed?.Invoke();
                gearC.SetGlowingMaterial();
                return;
            }
        }
    }

    public void ActivateGear()
    {
        if (stopPoint == null)
            return;

        StopAllCoroutines();

        if (inOriginalPoint)
        {
            StartCoroutine(Move(stopPoint.position));
            inOriginalPoint = false;
        }
        else
        {
            StartCoroutine(Move(originalPosition));
            inOriginalPoint = true;
        }
    }

    IEnumerator Move(Vector3 targetTransform)
    {
        while(Vector3.Distance(transform.position, targetTransform) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform, 5 * Time.deltaTime);
            yield return null;
        }
    }

    public void CheckGears()
    {
        //For Debug Purposes delete for performance
        bool hasSameGears = false;

        foreach (GameObject gear in ConnectedGears)
        {
            hasSameGears = ConnectedGears.Intersect(gear.GetComponent<Gear>().ConnectedGears).Any();
            if (hasSameGears)
            {
                gear.GetComponent<Gear>().Jammed = true;
            }
            else
                gear.GetComponent<Gear>().Jammed = false;

        }      

        //List<GameObject> sameGears = ConnectedGears.Intersect(otherGear.ConnectedGears).ToList();
        //if the other gear has the same gear ur connected to
        //bool hasSameGears = ConnectedGears.Intersect(otherGear.ConnectedGears).Any();

        //foreach (GameObject gear in sameGears)
        //{
        //    Debug.Log(otherGear.name + " and " + this.gameObject.name + " has " + gear.name);
        //}
    }

    public void CheckConnectedGears(GameObject ignore)
    {
        if (!Rotating)
        {
            Debug.Log("check gears " + gameObject.name);
            foreach (GameObject gear in ConnectedGears)
            {
                if (gear.GetComponent<Gear>().Jammed)
                    return;
                if (gear != ignore)
                {
                    gear.GetComponent<Gear>().Rotating = false;
                    gear.GetComponent<Gear>().CheckConnectedGears(this.gameObject);
                }
            }
            SetGlowingMaterial();
            return;
        }

        //bool rotate = ConnectedGears.Any(gear => gear.GetComponent<Gear>().Rotating);
        //if (rotate)
        //{
        //    Rotating = true;
        //}
        //else
        //    Rotating = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gear otherGear))
        {
            if (!ConnectedGears.Contains(other.gameObject))
            {
                ConnectedGears.Add(other.gameObject);
            }

            CheckGears();

            //StopAllCoroutines();

            if (!Rotating)
                return;

            if (otherGear.Jammed)
            {
                OnGearJammed?.Invoke();
                return;
            }

            if (otherGear.Rotating == false)
            {
                Debug.Log("Rot " + otherGear.name);
                otherGear.Rotate(RotationSpeed * -1);
                otherGear.SetGlowingMaterial();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Gear otherGear))
        {
            if (ConnectedGears.Contains(other.gameObject))
            {
                ConnectedGears.Remove(other.gameObject);
            }

            CheckGears();

            foreach (GameObject gear in ConnectedGears)
            {
                if (gear.GetComponent<Gear>().OriginallyRotating)
                {
                    return;
                }
            }

            Rotating = InitRotationState;
            GetComponent<Gear>().CheckConnectedGears(this.gameObject);
            SetGlowingMaterial();
        }

    }
}
