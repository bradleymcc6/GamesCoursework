using System.Collections;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    
    }

    void FixedUpdate()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            Physics.gravity = new Vector3(0, 4, 0);
        }
    }
        void OnTriggerExit(Collider other)
        {

                Physics.gravity = new Vector3(0, -9.81f, 0);
            
        }


}
