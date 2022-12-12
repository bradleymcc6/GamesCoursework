using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    public UnityEngine.Vector3 movement;
    public Transform orientation;
    public float moveSpeed = 7.0f;
    float hInput;
    float vInput;

    //Speed of walking character
    public float speed = 1000f;
    
    //Height of jump
    public float jumpHeight = 30;

    //Extent to which gravity pushes down
    public float gravityScale = 0.5f;


    public UnityEngine.Vector2 rotation;

    public UnityEngine.Vector3 direction;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        animator = GetComponent<Animator>();

    }


    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(UnityEngine.Vector2.up * jumpHeight, ForceMode.Impulse);
        }
        
        //If statement prevents calling to a rotation of 0,0
        if (movement != UnityEngine.Vector3.zero)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(movement);
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("CharacterSpeed", 1.0f);
        }
        else
        {
            animator.SetFloat("CharacterSpeed", 0.0f);
        }
    }



// Update is called once per frame
void FixedUpdate()
    {


        direction = orientation.forward * vInput + orientation.right * hInput;
        rigidBody.AddForce(direction.normalized * moveSpeed * 10.0f, ForceMode.Force);
        rigidBody.AddForce(Physics.gravity * (gravityScale - 1) * rigidBody.mass);
        rigidBody.angularVelocity = UnityEngine.Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {

        {
            rigidBody.velocity = UnityEngine.Vector3.zero;
            rigidBody.angularVelocity = UnityEngine.Vector3.zero;

        }
    }
}
