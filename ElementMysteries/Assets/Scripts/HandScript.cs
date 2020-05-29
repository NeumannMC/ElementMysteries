using ElemantMysteries;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{

    [SerializeField] private LayerMask interactionLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void cast(string cast) 
    {
        Ray ray = new Ray(transform.position,transform.forward);
        Debug.DrawLine(ray.origin, ray.GetPoint(5));

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,5, interactionLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("GameObject: "+hit.collider.gameObject.name);
            InteractiveHitbox it = hitObject.GetComponent<InteractiveHitbox>();
            Debug.Log("Script: "+it.name);
            Debug.Log(cast);
            it.getHit(cast);
            
            
        }
    }
}
