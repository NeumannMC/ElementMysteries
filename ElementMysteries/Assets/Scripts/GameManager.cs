using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElemantMysteries
{
    public class GameManager : MonoBehaviour
    {
        [Header("Static Attributes")]
        public static GameManager instance;
        public static Dictionary<string, Vector3> checkpoints;
        public static Dictionary<int, string> castElements;
        [SerializeField] private GameObject[] checkpointsP;
        [SerializeField] private string[] elements;
        void Awake()
        {

            //Singelton Setup
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                initAll();
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void initAll()
        {
            checkpoints = new Dictionary<string, Vector3>();
            castElements = new Dictionary<int, string>();
            initCheckpoints();
            initElements();
        }

        private void initElements()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] !="")
                {
                    castElements.Add(i, elements[i]);
                }
            }
            Debug.Log("GameManager elements: " + castElements.Count);
        }

        private void initCheckpoints()
        {
            for(int i =0;i< checkpointsP.Length; i++)
            {
                checkpoints.Add(checkpointsP[i].name, checkpointsP[i].transform.position);
            }

            Debug.Log("GameManager Chackpoints: "+checkpoints.Count);
        }
    }
}
