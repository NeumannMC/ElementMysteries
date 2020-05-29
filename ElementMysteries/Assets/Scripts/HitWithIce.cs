using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWithIce : MonoBehaviour
{
    
    [SerializeField]private string hittableCast;
    [SerializeField] private GameObject block; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit(string kindOfHit)
    {
        if (hittableCast.Equals(kindOfHit))
        {
            Debug.Log("Cast ist good ");
            if (!block.activeSelf)
            {
                Invoke("activateBlock", 3);
            }
        }
    }

    private void activateBlock() 
    {
        block.SetActive(true);
    }
}
