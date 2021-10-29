using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    //camera
    public Transform cam;

    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    public float gravity = 9.8f;

    [SerializeField]
    public float jumpspeed = 3.5f;

    [SerializeField] private float jumpHeight;


    private Vector3 velocity;

    //smooth turning
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //references

    private Animator anim;

    private float directionY;

    // Update is called once per frame

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        

        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //directionY -= gravity;

        direction.y = directionY;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //smooth turning
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }



    }


