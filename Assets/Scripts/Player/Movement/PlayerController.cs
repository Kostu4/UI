using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speedMovement = 12f;
    private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;

    public Transform GroundCheck;
    private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private LashStrike ability;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    void Start()
    {
        ability = FindObjectOfType<LashStrike>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ability.UseAbility();
        }
        //Ground check
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        //Reseting the default velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //(right - red axis, forward - blue axis)

        //Actually moving the player
        controller.Move(move * speedMovement * Time.deltaTime);

        //Check if the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Actually jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Falling down
        velocity.y += gravity * Time.deltaTime;

        //Executing the jump
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
            //for later use
        }
        else
        {
            isMoving = false;
            //for later use
        }
        lastPosition = gameObject.transform.position;
    }

}
