using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defuse : MonoBehaviour
{
   

        [SerializeField]
        public Slider slider;

        public GameObject GameWon;
        

        public void SetProgress(int progress)
        {
            slider.value = progress;

            if (slider.value ==80)
            {
                GameWon.SetActive(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

