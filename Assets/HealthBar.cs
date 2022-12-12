using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    public Slider slider;

    public GameObject GameOver;

    public void SetHealth(int health)
    {
        slider.value = health;

        if (slider.value <= 0)
        {
            GameOver.SetActive(true);
        }
    }



    public void ResetHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
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
