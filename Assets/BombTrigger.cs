using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BombTrigger : MonoBehaviour
{
    public GameObject holdToDefuse;
    public GameObject[] finalEnemies;
    bool isTriggered = false;
    public GameObject enemy;
    public Transform player;
    
    public Defuse defuse;
    public int progress = 0;
    public GameObject defuseProgress;
    bool finalEnemy;
    bool coroutineRunning;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            holdToDefuse.SetActive(true);
            isTriggered = true;

            //Ensures final enemy only spawns once
            if (finalEnemy != true)
            {
                Instantiate(enemy, new Vector3(170f, 20f, 200f),
                             Quaternion.identity);
                finalEnemy = true;
            }
        }
        
           
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            holdToDefuse.SetActive(false);
            defuseProgress.SetActive(false);
           
            progress = 0;
            isTriggered = false;

            //Stop progress if player steps away from bomb
            if (coroutineRunning)
            {
                StopCoroutine(IncreaseProgress());
            }

        }


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            
            if (Input.GetKey(KeyCode.E))
            {
                holdToDefuse.SetActive(false);
               
                defuseProgress.SetActive(true);

                //Ensures we do not have multiple coroutines running simultaneously
                if (!coroutineRunning)
                {


                    StartCoroutine(IncreaseProgress());

                }
                
        }

        }
    }


    IEnumerator IncreaseProgress()
    {
        coroutineRunning = true;
if (Input.GetKey(KeyCode.E))
            {

        for (int i = 0; i < 81; i++)
        {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    i = 0;
                }


                progress = i;
                defuse.SetProgress(progress);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
