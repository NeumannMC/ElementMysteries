using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlattform : MonoBehaviour
{
    #region private Fields
    private Vector3 moveDirection;
    private Vector3 startPosition;
    [SerializeField] private float movingSpeed;
    private int currentDirection;
    private Vector3[] directrions = { new Vector3(0,0,-1),new Vector3(0, 0, 1),new Vector3(1, 0, 0),new Vector3(-1, 0, 0) };
    private GameObject playerOnPlattform;
   
    #endregion
    
    void Awake()
    {
        startPosition = new Vector3(-3, 1, 36.5f);
        moveDirection = new Vector3(0,0,-1);
        currentDirection = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + (moveDirection * movingSpeed * Time.deltaTime) ;       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("HitBox" + currentDirection))
        {
            if (currentDirection + 1 == 4)
            {
                currentDirection = -1;
            }

            if(currentDirection==3 && playerOnPlattform !=null)
            {
                playerOnPlattform.GetComponent<CharacterController>().enabled=true;
            }

            currentDirection += 1;
            moveDirection = directrions[currentDirection];

        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLayer");
            
           
            
            other.transform.parent = this.gameObject.transform;
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            playerOnPlattform = null;
        }
    }


}
