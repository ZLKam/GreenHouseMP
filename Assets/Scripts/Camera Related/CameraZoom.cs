using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform parentObject;
    public float zoomLevel;
    public float sensitivity = 1;
    public float speed = 30;
    public float maxZoom = 30;
    float zoomPosition;

    private float initialDistance;

    void Start()
    {
        speed = PlayerPrefs.GetFloat("zoomSpeed");
        sensitivity = PlayerPrefs.GetFloat("zoomSensitivity");
    }

    void Update()
    {
        Zoom();
        ZoomInput();
    }

    void Zoom()
    {
        zoomLevel += Input.mouseScrollDelta.y * sensitivity;
        zoomLevel = Mathf.Clamp(zoomLevel, 0, maxZoom);
        zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
        transform.position = parentObject.position + (transform.forward * zoomPosition);

        ClipCheck();
    }

    void ZoomInput()
    {
        if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus))
        {
            zoomLevel += sensitivity;
            zoomLevel = Mathf.Clamp(zoomLevel, 0, maxZoom);
            zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
            transform.position = parentObject.position + (transform.forward * zoomPosition);
        }

        if(Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            zoomLevel -= sensitivity;
            zoomLevel = Mathf.Clamp(zoomLevel, 0, maxZoom);
            zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
            transform.position = parentObject.position + (transform.forward * zoomPosition);
        }
    }

    void ClipCheck()
    {
        Ray ray = new Ray(parentObject.position, transform.forward);
        if (Physics.SphereCast(ray, 1, out RaycastHit hit, maxZoom))
        {
            if (hit.distance < zoomLevel + 3)
            {
                zoomLevel = hit.distance - 3;
            }
        }
    }
}

