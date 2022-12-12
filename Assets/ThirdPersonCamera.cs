using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    [SerializeField]
    int mouseSensitivity;
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rigidbody;
    public Transform LookAt;

    public float rotationSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //This establishes the direction the camera is facing and sets this as the character's forward position
        Vector3 cameraDirection = LookAt.position - new Vector3(transform.position.x, LookAt.position.y, transform.position.z);
        orientation.forward = cameraDirection.normalized;
        playerObject.forward = cameraDirection.normalized;

        //This allows us to rotate the player
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = orientation.forward * (vInput*mouseSensitivity) + orientation.right * (hInput*mouseSensitivity);
       
        //Only sets player direction if input direction is not already set to 0
        if (direction != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, direction.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
