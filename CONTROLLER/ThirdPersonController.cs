﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public bool controlOn = GameManager.IsInputEnabled;

    public Camera MyCamera;
    public float Speed = 3f;
    public float SprintSpeed = 6f;
    public float RotationSpeed = 15f;
    public float AnimationBlendSpeed = 5f;
    public float JumpSpeed = 15f;


    CharacterController MyController;
    Animator MyAnimator;

    float mDesiredRotation = 0f;
    float mDesiredAnimationSpeed = 0f;
    bool mSprinting = false;

    float mSpeedY = 0;
    float mGravity = -9.81f;

    bool mJumping = false;

    void Start()
    {
        MyController = GetComponent<CharacterController>();
        MyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        if(GameManager.IsInputEnabled = true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Jump") && !mJumping)
            {
                mJumping = true;
                MyAnimator.SetTrigger("Jump");

                mSpeedY += JumpSpeed;
            }

            if (!MyController.isGrounded)
            {
                mSpeedY += mGravity * Time.deltaTime;
            }
            else if (mSpeedY < 0)
            {
                mSpeedY = 0;
            }

            MyAnimator.SetFloat("SpeedY", mSpeedY / JumpSpeed);

            if (mJumping && mSpeedY < 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Ground")))
                {
                    mJumping = false;
                    //MyAnimator.SetTrigger("Land");
                }
            }
            mSprinting = Input.GetKey(KeyCode.LeftShift);

            Vector3 movement = new Vector3(x, 0, z).normalized;

            Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y, 0) * movement;
            Vector3 verticalMovement = Vector3.up * mSpeedY;

            MyController.Move((verticalMovement + (rotatedMovement * (mSprinting ? SprintSpeed : Speed))) * Time.deltaTime);


            if (rotatedMovement.magnitude > 0)
            {
                mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
                mDesiredAnimationSpeed = mSprinting ? 1 : .5f;
            }
            else
            {
                mDesiredAnimationSpeed = 0;

            }

            MyAnimator.SetFloat("Speed", Mathf.Lerp(MyAnimator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));


            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
        }

    }
}
