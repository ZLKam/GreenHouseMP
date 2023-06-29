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
    //sets the skybox of the game level based on the int
    //selects a background based on the index, array of backgrounds is set in the variables
    {
        RenderSettings.skybox = skyboxes[PlayerPrefs.GetInt("backgroundIndex")];
    }

    void EnhancedCamera()
    //not used, intended purpose?
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
    //switches the camera's rotation based on different points in the scene
    //allows movement of the camera getting it from input, applying the vector direction then set it across the delta time
    {
        transform.LookAt(cameras[selectedCamera]);
#if UNITY_STANDALONE
        transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            //if the finger has moved
            //checks the difference between the intial touch position of the finger to the new finger position
            {
                transform.Translate(Vector3.up * Input.GetTouch(0).deltaPosition.y);
                transform.Translate(Vector3.right * Input.GetTouch(0).deltaPosition.x);
            }
        }
#endif
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
    //switches camera between different transforms in the scene which provides slightly altered view of the game
    //not sure what is the goal but seems to switch intended view of the level, but can already customise view
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
