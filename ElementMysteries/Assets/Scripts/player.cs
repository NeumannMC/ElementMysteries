using ElemantMysteries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class player : MonoBehaviour
{
    [Header("Private Fields but Serializable")]
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject hand2;
    [SerializeField] private GameObject hand3;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject Waterprefab;
    [SerializeField] private GameObject[] spells;
    [SerializeField] private float Life;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject sideBarUI;
    [SerializeField] private GameObject topBar;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private GameObject crossAir;


    private float currentLife;
    private float timer =3;
    private float timer2 = 3;
    private GameObject hand1VisualEffect;
    private GameObject hand2VisualEffect;
    private int selectedSpellH1;
    private int selectedSpellH2;
    private bool castwithBoth = false;
    private Dictionary<string, GameObject> inventar;
    private string currentCheckpoint;
    private Collection<string> goalDoor;
    private Ray rayHand3;
    private RaycastHit hitHand;

    void Awake()
    {
        
        inventar = new Dictionary<string, GameObject>();
        currentLife = Life;
        selectedSpellH1 = 0;
        selectedSpellH2 = 0;
        initDoorGoal();
       
    }

    private void initDoorGoal()
    {
        goalDoor = new Collection<string>();
        goalDoor.Add("FireKristall");
        goalDoor.Add("WaterKristall");
        goalDoor.Add("WindKristall");
        Debug.Log(goalDoor.Count);
    }

    // Update is called once per frame
    void Update()
    {

        rayHand3 = new Ray(hand3.transform.position, hand3.transform.forward);
        if (Physics.Raycast(rayHand3,out hitHand, 5, interactionLayer))
        {
            crossAir.GetComponent<Image>().color = Color.red;
        }
        else
        {
            crossAir.GetComponent<Image>().color = Color.white;
        }

        //Check RightClick
        if (Input.GetMouseButton(0))
        {
            if (hand1VisualEffect == null)
            {

                //Destroy(VFXObject.GetComponent<VisualEffect>());
                hand1VisualEffect = Instantiate(spells[selectedSpellH1], hand.transform.position, hand.transform.rotation);
                hand1VisualEffect.transform.SetParent(hand.transform);

            }

            if (timer <= 0)
            {
                Debug.Log("cast One");
                Debug.Log(castwithBoth);
                if (!castwithBoth)
                {
                    Debug.Log("Cast 1");
                    string castElement;
                    if (GameManager.castElements.TryGetValue(selectedSpellH1, out castElement))
                    {
                        Debug.Log("Cast with " + castElement);
                        hand.GetComponent<HandScript>().cast(castElement);
                    }
                    else
                    {
                        Destroy(hand1VisualEffect);
                        hand1VisualEffect = null;
                    }
                }
                timer = 3;
            }

            timer -= Time.deltaTime;

        }
       else if(!Input.GetMouseButton(0) && hand1VisualEffect != null)
        {
            Destroy(hand1VisualEffect);
            hand1VisualEffect = null;
            timer = 3;
            castwithBoth = false;
        }


        //Check LefClick
        if (Input.GetMouseButton(1))
        {
            if (hand2VisualEffect == null)
            {

                //Destroy(VFXObject.GetComponent<VisualEffect>());
                hand2VisualEffect = Instantiate(spells[selectedSpellH2], hand2.transform.position, hand2.transform.rotation);
                hand2VisualEffect.transform.SetParent(hand2.transform);

            }

            if (timer2 <= 0)
            {
                Debug.Log("cast Two");
                
                if (!castwithBoth)
                {
                    Debug.Log("Cast 2");
                    string castElement;
                    if (GameManager.castElements.TryGetValue(selectedSpellH2, out castElement))
                    {
                        
                        hand2.GetComponent<HandScript>().cast(castElement);
                    }
                    else
                    {
                        Destroy(hand2VisualEffect);
                        hand2VisualEffect = null;
                    }
                }
                timer2 = 3;
            }

            timer2 -= Time.deltaTime;
        }
        else if (!Input.GetMouseButton(1) && hand2VisualEffect != null)
        {
            Destroy(hand2VisualEffect);
            hand2VisualEffect = null;
            timer2 = 3;
            castwithBoth = false;
        }

       
        
        if(hand1VisualEffect != null && hand2VisualEffect != null)
        {
            if (checkPossibleCombination(selectedSpellH1+1,selectedSpellH2+1))
            {
                //Debug.Log("cast With Both");
                castwithBothHAnd();
                castwithBoth = true;
            }
        }

       /* if (Input.GetKeyDown(KeyCode.T))
        {
            this.GetComponent<CharacterController>().enabled = false;
            transform.localPosition = new Vector3(0, 3, 0);
            this.GetComponent<CharacterController>().enabled = true;
            
        }*/

        if(Input.mouseScrollDelta.y == 1)
        {
            if (Input.GetKey(KeyCode.LeftShift)) {
                
                //Debug.Log("Next Element Hand 2");
                setNextElement(1, 2);
            }
            else
            {
                //Debug.Log("Next Element Hand 1");
                setNextElement(1, 1);
            }
            
        }
        else if (Input.mouseScrollDelta.y == -1) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                //Debug.Log("Next Element Hand 2");
                setNextElement(-1, 2);
            }
            else
            {
                //Debug.Log("Next Element Hand 1");
                setNextElement(-1, 1);
            }
        }
        

    }

    private void castwithBothHAnd()
    {
        string castElement;
        if (GameManager.castElements.TryGetValue(selectedSpellH1 + 1 + selectedSpellH2 + 1, out castElement))
        {
            hand3.GetComponent<HandScript>().cast(castElement);
        }
        else
        {
            Destroy(hand1VisualEffect);
            hand1VisualEffect = null;
            Destroy(hand1VisualEffect);
            hand1VisualEffect = null;

            timer = 3;
            timer2 = 3;
        }

        castwithBoth = false;
        Debug.Log("Neue Werte"+selectedSpellH1+" "+ selectedSpellH2);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Lava"))
        {
          Debug.Log("Hit Lava");
            
            Vector3 t;
            if (GameManager.checkpoints.TryGetValue(currentCheckpoint, out t))
            {
                Debug.Log("Check");
                Debug.Log(t);
            }
            else
            {
                Debug.Log("Something got wrong");
                t = new Vector3(0, 2, 0);
            }
            this.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.localPosition = t;
            this.GetComponent<CharacterController>().enabled = true;
            takeHit(1);
        }

        if (other.CompareTag("Getable"))
        {
            //Debug.Log("GetItem");
            inventar.Add(other.gameObject.name, other.gameObject);
            deleteGoalItem(other.gameObject.name);
            sideBarUI.GetComponent<SideBar>().enableKristallImage(other.gameObject.name);
            //Debug.Log(inventar.Count);
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Checkpoint"))
        {
            //Debug.Log("Get " + other.gameObject.name);
            currentCheckpoint = other.name;
            //Debug.Log("CurrentSwpanPoint is "+currentCheckpoint);
            
        }
        if (other.CompareTag("Door"))
        {
            if (checkGoalDoor())
            {
                other.GetComponent<HitboxDoor>().openDoor();
            }
        }
        if (other.CompareTag("DeadZone"))
        {
            Vector3 t;
            if (GameManager.checkpoints.TryGetValue(currentCheckpoint, out t))
            {
                //Debug.Log("Check");
                //Debug.Log(t);
            }
            else
            {
               // Debug.Log("Something got wrong");
                t = new Vector3(0, 2, 0);
            }
            this.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.localPosition = t;
            this.GetComponent<CharacterController>().enabled = true;
            takeHit(1);
        }

        if (other.CompareTag("Goal"))
        {
            win();
        }

    }

    private bool checkGoalDoor()
    {
        if (goalDoor.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void deleteGoalItem(string pName)
    {
        if (goalDoor.Contains(pName))
        {
            Debug.Log("Contains");
            goalDoor.RemoveAt(goalDoor.IndexOf(pName));
        }
        Debug.Log(goalDoor.Count);
    }

    private void takeHit(float pDamage)
    {
        currentLife = currentLife - pDamage;
        topBar.GetComponent<TopBar>().updateHealth((int)currentLife);
        kill();
    }
    private void kill()
    {
        if (currentLife <= 0)
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.localPosition = new Vector3(0,2,0);
            this.GetComponent<CharacterController>().enabled = true;
            Invoke("lose", 1);

        }
    }

    public bool checkInventar(string[] collection)
    {
        bool collectionInInventar = true;
        for(int i = 0; i < collection.Length; i++)
        {
            if (inventar.ContainsKey(collection[i]))
            {
                collectionInInventar = true;
            }
            else
            {
                collectionInInventar = false;
            }
        }

        return collectionInInventar;
        
    }

    private void setNextElement(int nextIndexDirect ,int hand)
    {
        switch (hand)
        {
            case 1:
                selectedSpellH1 = getNextPossibleElementIndex(selectedSpellH1,nextIndexDirect);
                ui.GetComponent<UIScript>().UpdateElementImage(selectedSpellH1, hand);
                break;
            case 2:
                selectedSpellH2 = getNextPossibleElementIndex(selectedSpellH2,nextIndexDirect);
                ui.GetComponent<UIScript>().UpdateElementImage(selectedSpellH2, hand);
                break;
        }
    }

    private int getNextPossibleElementIndex(int start,int direction)
    {
        int index = -1;
        int pos=start;
       

        while(index == -1 )
        {
           pos= pos + direction;
            if (pos < 0)
            {
                pos = spells.Length - 1;
            }
            else if (pos>=spells.Length)
            {
                pos = 0;
            }
            if (spells[pos] != null)
            {
                index = pos;
            }
            if (pos == start)
            {
                index = 0;
            }
        }

        return index;
    }
  

    private bool checkPossibleCombination(int firstHand,int secondHand)
    {
        if(firstHand==3 || firstHand==2 || secondHand == 3 || secondHand == 2)
        {
            if (firstHand + secondHand == 5)
            {
            return true;
            }
        }
        return false;
    }

    private void win()
    {
        SceneManager.LoadScene("Menu");
    }
    private void lose()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
