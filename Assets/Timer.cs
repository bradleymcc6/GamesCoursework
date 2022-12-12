using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float amountOfTimeRemaining;
    public bool timeRemaining;

    public GameObject gameOverObject;

    public Text Text;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = true;
        amountOfTimeRemaining = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining)
        {
            if(amountOfTimeRemaining >= 0)
            {
                amountOfTimeRemaining -= Time.deltaTime;
                TimerUpdate(amountOfTimeRemaining);
            }
            else
            {
                amountOfTimeRemaining = 0f;
                timeRemaining = false;
                gameOverObject.SetActive(true);
            }
        }
    }

    void TimerUpdate(float time)
    { 

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        Text.text = "Time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
