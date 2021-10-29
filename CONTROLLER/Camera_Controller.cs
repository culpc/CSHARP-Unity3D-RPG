﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //variables
    [SerializeField] private float mouseSensitivity;

    //references
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);

        //turning animation
        //anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
}
