using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDoor : MonoBehaviour
{
    public GameObject doorR;
    public GameObject doorL;


    public void openDoor()
    {
        doorR.GetComponent<DoorMovement>().openDoor();
        doorL.GetComponent<DoorMovement>().openDoor();
    }
}
