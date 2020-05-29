using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    [SerializeField] private float rangeX;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeTolarance;

    private Vector3 startPosition;
    private Vector3 direct = new Vector3(1,0, 0);

    private bool active; 
    private bool isClosed = true;
    private bool isOpening=false;
    private bool isCloisng= false;

    

    void Start()
    {
        startPosition = transform.position;
        
        if (transform.name.Equals("FrontGateL"))
        {
            Debug.Log("Juhu");
            direct = direct * -1;
        }
        active = false;
    }

    void FixedUpdate()
    {
        if (!isClosed)
        {
            if (isOpening && !isCloisng)
            {
                if (transform.name.Equals("FrontGateL"))
                {
                    if (transform.position.x <= startPosition.x - rangeX + rangeTolarance)
                    {
                        isOpening = false;
                        Invoke("changeCloseing", 7);
                    }
                    else
                    {
                        float newPosX = transform.position.x + movementSpeed * direct.x * Time.deltaTime;
                        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
                    }
                }
                else
                {

                    if (transform.position.x >= startPosition.x + rangeX - rangeTolarance)
                    {
                        isOpening = false;
                        Invoke("changeCloseing", 7);
                    }
                    else
                    {
                        float newPosX = transform.position.x + movementSpeed * direct.x * Time.deltaTime;
                        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
                    }
                }

            }else if ( !isOpening && isCloisng)
            {
                if (transform.name.Equals("FrontGateL"))
                {
                    if (transform.position.x >= startPosition.x - rangeTolarance)
                    {
                        isCloisng = false;
                        isClosed = true;
                        active = false;
                        direct = direct * -1;
                        transform.position = startPosition;
                    }
                    else
                    {
                        float newPosX = transform.position.x + movementSpeed * direct.x * Time.deltaTime;
                        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (transform.position.x <= startPosition.x + rangeTolarance)
                    {
                        isCloisng = false;
                        isClosed = true;
                        active = false;
                        direct = direct * -1;
                        transform.position = startPosition;
                    }
                    else
                    {
                        float newPosX = transform.position.x + movementSpeed * direct.x * Time.deltaTime;
                        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
                    }
                }
            }

        }

       

    }

    public void openDoor()
    {
        if (!active)
        {
            isClosed = false;
            isOpening = true;
            active = true;
        }

        
    }

    private void changeCloseing()
    {
        direct = direct * -1;
        isCloisng = true;
    }

  
}
