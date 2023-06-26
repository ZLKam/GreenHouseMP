using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform[] cameras;
    int selectedCamera;

    float originalSpeed;
    public float rotationSpeed;

    float rotationX;
    float rotationY;

    float xRotation;
    float yRotation;

    public List<Material> skyboxes;

    void Start()
    {
        //PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        /*
        if (level1)
        {
            
        }

        if (level2)
        {
            PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        }
        */
        rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed");
        originalSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkybox();
        SwitchCamera();
        TemporaryCamera();
        //EnhancedCamera();
    }

    void CheckSkybox()
    {
        RenderSettings.skybox = skyboxes[PlayerPrefs.GetInt("backgroundIndex")];
    }

    void EnhancedCamera()
    {
        transform.LookAt(cameras[selectedCamera]);
        //transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
        //float moveX = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

        xRotation -= moveY;
        xRotation = Mathf.Clamp(xRotation, -10f, 10f);

        transform.localRotation = Quaternion.Euler(xRotation * rotationSpeed, 0f, 0f);
    }

    void TemporaryCamera()
    {
        transform.LookAt(cameras[selectedCamera]);
        transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rotationSpeed = originalSpeed * 2;
        }
        else
        {
            rotationSpeed = originalSpeed;
        }
            
        
    }

    //void RotateCamera()
    //{
    //    float moveX = Input.GetAxisRaw("Horizontal") * rotationSpeed;
    //    float moveY = Input.GetAxisRaw("Vertical") * rotationSpeed;

    //    rotationX += moveX;
    //    rotationY += moveY;
    //    rotationX = Mathf.Clamp(rotationX, -40, 40);

    //    transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

    //    transform.position = target.position - transform.forward;
    //}


    void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(selectedCamera > 0)
            {
                selectedCamera--;
            }
            else
            {
                selectedCamera = cameras.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedCamera < cameras.Length - 1)
            {
                selectedCamera++;
            }
            else
            {
                selectedCamera = 0;
            }
        }
    }
}
