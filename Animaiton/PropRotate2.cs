using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotate2 : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.right * speed * Time.deltaTime);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}
