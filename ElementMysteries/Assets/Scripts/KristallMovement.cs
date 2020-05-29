using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KristallMovement : MonoBehaviour
{

    [SerializeField] private float rangeY;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rangeTolarance;

    private Vector3 startPosition;
    private Vector3 direct= new Vector3(0, 1, 0);
   
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.position.y >= startPosition.y + rangeY +rangeTolarance || transform.position.y <= startPosition.y-rangeTolarance)
        {
            direct=direct * -1;
        }

        float newPosY = transform.position.y + movementSpeed * direct.y * Time.deltaTime;
        transform.position = new Vector3(transform.position.x,newPosY, transform.position.z);

        transform.rotation = transform.rotation * Quaternion.Euler(0, rotationSpeed, 0);
       
       
    }
}
