using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;

    public void UpdateElementImage(int index,int pHand)
    {
        switch(pHand){

            case 1: GameObject g = hand1.transform.Find("ElementImage").gameObject;
                g.GetComponent<panelScript>().updateImage(index);
                break;

            case 2:
                Debug.Log("Hand2 UI");
                GameObject g2 = hand2.transform.Find("ElementImage").gameObject;
                Debug.Log(g2.transform.parent.name);
                g2.GetComponent<panelScript>().updateImage(index);
                break;
        }
    }
}
