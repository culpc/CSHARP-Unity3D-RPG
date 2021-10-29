using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;


    Vector2 inputs;
    float rotation;

    public float baseSpeed = .025f, runSpeed = 4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Locomotion();
    }

    void Locomotion()
    {

    }

    void GetInputs()
    {
        //forwards-backwards controls
        //forwards
        if (Input.GetKey(controls.forwards))
            inputs.y = 1;

        //backwards
        if (Input.GetKey(controls.backwards))
        {
            if (Input.GetKey(controls.forwards))
                inputs.y = 0;
            else
                inputs.y = -1;
        }

        //fw nothing
        if ((!Input.GetKey(controls.forwards)) && (!Input.GetKey(controls.backwards)))
                inputs.y = 0;

        //strafe left right
        //strafe left
        if (Input.GetKey(controls.strafeRight))
            inputs.x = 1;

        //strafe right
        if (Input.GetKey(controls.strafeLeft))
        {
            if (Input.GetKey(controls.strafeRight))
                inputs.x = 0;
            else
                inputs.x = -1;
        }

        //strafe lr nothing
        if ((!Input.GetKey(controls.strafeLeft)) && (!Input.GetKey(controls.strafeRight)))
                inputs.x = 0;

        //rotate left right
        //rotate left
        if (Input.GetKey(controls.rotateRight))
            rotation = 1;

        //rotate right
        if (Input.GetKey(controls.rotateLeft))
        {
            if (Input.GetKey(controls.rotateRight))
                rotation = 0;
            else
                rotation = -1;
        }

        //rotate lr nothing
        if ((!Input.GetKey(controls.rotateLeft)) && (!Input.GetKey(controls.rotateRight)))
                rotation = 0;

    }
}
