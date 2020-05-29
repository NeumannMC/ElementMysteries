using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveHitbox : MonoBehaviour
{
   
    [SerializeField] private string hitableCast;
    

    public void getHit(string kindOfHit)
    {
        if (hitableCast.Equals(kindOfHit))
        {


            switch (kindOfHit)
            {
                case "Ice":
                    hitWithIce();
                    break;
                case "Wind": hitWithWind();
                    break;
                case "Fire": hitWithFire();
                    break;
                case "Water": hitWithWater();
                    break;
                default: Debug.Log("Not Right");
                    break;
            }
        }
    }

    private void hitWithWater()
    {
        throw new NotImplementedException();
    }

    private void hitWithFire()
    {
        
        Invoke("rotatePalm", 3);
        


    }

    private void hitWithWind()
    {
        Debug.Log("Hit with Wind");
        GameObject go = this.transform.GetChild(0).gameObject;
        go.GetComponent<ParticleSystem>().Stop();
        toggleLabyrinth();
        this.GetComponent<Collider>().enabled = false;
        Invoke("startDust", 15);
    }

    private void hitWithIce()
    {
        Debug.Log(name);
        Invoke("activateBlock", 3);

    }

    private void activateBlock()
    {
        GameObject go = this.transform.GetChild(0).gameObject;
        go.SetActive(true);
    }

    private void startDust()
    {
        GameObject go = this.transform.GetChild(0).gameObject;
        go.GetComponent<ParticleSystem>().Play();
        Invoke("toggleLabyrinth", 3);
        Invoke("enableCollider", 3);
        
    }
    private void toggleLabyrinth()
    {
       GameObject labyrinth = GameObject.Find("Labyrinth");
       //Debug.Log(labyrinth.name);
       
       for(int i=0; i< labyrinth.transform.childCount; i++)
        {
            labyrinth.transform.GetChild(i).GetComponent<Renderer>().enabled = !labyrinth.transform.GetChild(i).GetComponent<Renderer>().enabled;
        }

    }

    private void enableCollider()
    {
        this.GetComponent<Collider>().enabled = true;
    }

    private void rotatePalm()
    {
        if (!this.GetComponent<Collider>().enabled == false)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Palm");
            go.transform.rotation = go.transform.rotation * Quaternion.Euler(0, 0, 75);
            this.GetComponent<Collider>().enabled = false;
        }
        
    }
}
