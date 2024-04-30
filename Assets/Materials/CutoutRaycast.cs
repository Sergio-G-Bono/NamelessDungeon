using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        /*
        //    Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        //    cutoutPos.y /= (Screen.width / Screen.height);

        //    Vector3 offset = targetObject.position - transform.position;
        //    RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        //    for (int i = 0; i < hitObjects.Length; ++i)
        //    {
        //        Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
        //        Debug.Log(hitObjects[i].transform.gameObject.name);
        //        for (int m = 0; m < materials.Length; ++m)
        //        {

        //            materials[m].SetVector("_CutoutPos", cutoutPos);
        //            materials[m].SetFloat("_CutoutSize", 0.25f);
        //            materials[m].SetFloat("_FalloffSize", 0.25f);
        //        }
        //    }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Material r = other.transform.GetComponent<Renderer>().material;
        if (r != null)
        {
            r.SetVector("_CutoutPos", cutoutPos);
            r.SetFloat("_CutoutSize", 0.25f);
            r.SetFloat("_FalloffSize", 0.25f);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Material r = other.transform.GetComponent<Renderer>().material;
        if (r != null)
        {
            r.SetFloat("_CutoutSize", 0f);
            r.SetFloat("_FalloffSize", 0f);
        }

    }
}