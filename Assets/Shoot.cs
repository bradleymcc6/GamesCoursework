using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    public HealthBar healthBar;
    public GameObject Red;
    void resetAnimator()
    {
        animator.SetBool("CharacterAim", false);
        laserLine.enabled = false;
    }
    public Transform player;
    public float range = 100f;
    private Animator animator;
    int currentHealth;
    public Transform gunEnd;
    Color colour;

    private LineRenderer laserLine;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = 80;
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            ShootWeapon();


        if(Red !=null)
        {
            if (Red.GetComponent<Image>().color.a > 0)
            {


                colour = Red.GetComponent<Image>().color;
                colour.a -= 0.01f;
                Red.GetComponent<Image>().color = colour;

            }
        }

        

    }

    void ShootWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range))
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, gunEnd.position);
            EnemyAI enemyAIHit = hit.transform.GetComponent<EnemyAI>();
            laserLine.SetPosition(1, hit.point);
            animator.SetBool("CharacterAim", true);

            if (enemyAIHit != null)
            {
                if (PlayerPrefs.GetInt("Easy") == 1)
                {
                    enemyAIHit.TakeDamage(20);
                }
                else
                {
                    enemyAIHit.TakeDamage(10);
                }
                
            }
           
            Invoke("resetAnimator", 2);
        }
    }


    public void TakeDamage(int amount)
    {
        var colour = Red.GetComponent<Image>().color;
        colour.a = 0.3f;
        Red.GetComponent<Image>().color = colour;
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
       



        //if (health <= 0)
        //{

       // }
}

    public void TakeHeadDamage(int amount)
    {

    }
}
