using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Water : MonoBehaviour
{

    public Rigidbody rb;
    

    void Start()
    {
        rb = rb.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            rb.drag = 28;
        }
    }
    void OnTriggerExit(Collider other)
    {

        rb.drag = 8;

    }

}
