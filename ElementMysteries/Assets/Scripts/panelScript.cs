using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class panelScript : MonoBehaviour
{

    public Sprite[] sprites;
   

    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = sprites[0];
       
    }

   public void updateImage(int index)
    {
        this.GetComponent<Image>().sprite = sprites[index];
    }

}
