using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
   // {

        //transform.position = player.transform.position + new UnityEngine.Vector3(0, 2, -4);
        //transform.forward = player.transform.position - transform.position;
        void LateUpdate()
        {
            transform.position = player.transform.position - player.transform.forward * 6.0f;
            transform.LookAt(player.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        }
    //}
    }

