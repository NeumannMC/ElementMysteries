using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBar : MonoBehaviour
{
    [SerializeField] private GameObject goalPanel;
    [SerializeField] private GameObject FireKristallImage;
    [SerializeField] private GameObject WaterKristallImage;
    [SerializeField] private GameObject WindKristallImage;

    private string baseTextDiaQuest= ">Finde alle Kristalle! ";
    private int anzahlKristalle=0;


    public void enableKristallImage(string Kristall)
    {
        switch (Kristall)
        {
            case "FireKristall": FireKristallImage.GetComponent<Image>().enabled = true;
                anzahlKristalle++;
                updateQuestText();
                break;
            case "WaterKristall": WaterKristallImage.GetComponent<Image>().enabled = true;
                anzahlKristalle++;
                updateQuestText();
                break;
            case "WindKristall":WindKristallImage.GetComponent<Image>().enabled = true;
                anzahlKristalle++;
                updateQuestText();
                break;
        }
    }

    private void updateQuestText()
    {
        goalPanel.transform.Find("Quest1").GetComponent<Text>().text = baseTextDiaQuest + anzahlKristalle + "/3";
    }
   
}
