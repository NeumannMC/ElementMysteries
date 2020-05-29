using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    [SerializeField] private GameObject[] health;
    [SerializeField] private Sprite[] sprites;
    

    public void updateHealth(int Life)
    {
        health[Life].GetComponent<Image>().sprite = sprites[1];
    }
}
