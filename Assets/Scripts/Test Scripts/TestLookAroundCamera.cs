using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLookAroundCamera : MonoBehaviour
{
    private float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float rotationAmountY = 0.0f;
        float rotationAmountX = 0.0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            rotationAmountY = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
            rotationAmountX = -Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime;
        }
        else
        {
            rotationAmountY = Input.GetAxis("Mouse X") * rotationSpeed * 50 * Time.deltaTime;
            rotationAmountX = -Input.GetAxis("Mouse Y") * rotationSpeed * 50 * Time.deltaTime;
        }

        transform.Rotate(rotationAmountX, rotationAmountY, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
    }
}
