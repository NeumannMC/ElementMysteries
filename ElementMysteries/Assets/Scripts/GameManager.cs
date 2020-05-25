using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElemantMysteries
{
    public class GameManager : MonoBehaviour
    {
        [Header("Static Attributes")]
        public static GameManager instance;

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
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
