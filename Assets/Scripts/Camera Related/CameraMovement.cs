using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform[] cameras;
    int selectedCamera;
    public Placement placement;
    public Hover hover;

    float originalSpeed;
    public float rotationSpeed;

    float xRotation;
    float yRotation;

    public float initialDistance;
    public float sensitivity = 5;
    public float maxZoom = 140;
    public List<Material> skyboxes;
    public bool zooming;
    public bool moved;

    private Vector2 startPos;
    public float zoomStopDistance = 60;
    [SerializeField]
    private float zoomAmount;
    private float deltaDistance;
    private float tempRotation;

    private float maxRotationX = 30f;
    private float minRotationX = 330f;
    private float maxRotationY = 150f;
    private float minRotationY = 30f;

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
        sensitivity = PlayerPrefs.GetFloat("zoomSensitivity");
        originalSpeed = rotationSpeed;

        Camera.main.fieldOfView = 40f;
        zoomAmount = Camera.main.fieldOfView;
        StartCoroutine(ZoomCameraCoroutine());
    }

    private IEnumerator ZoomCameraCoroutine()
    {
        while (true)
        {
            Camera.main.fieldOfView -= sensitivity * 10f * Time.deltaTime;
            if (Camera.main.fieldOfView <= 25)
            {
                Camera.main.fieldOfView = 25;
                zoomAmount = Camera.main.fieldOfView;
                StopCoroutine(ZoomCameraCoroutine());
                break;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        CheckSkybox();
        if (placement != null)
        {
            if (!placement.deletingObject)
            //for level 1
            {
                SwitchCamera();
                CameraRotation();
                ZoomCamera();
            }
        }
        else
        //level 2
        {
            SwitchCamera();
            CameraRotation();
            ZoomCamera();
        }
        //EnhancedCamera();
    }

    void CheckSkybox()
    //sets the skybox of the game level based on the int
    //selects a background based on the index, array of backgrounds is set in the variables
    {
        RenderSettings.skybox = skyboxes[PlayerPrefs.GetInt("backgroundIndex")];
    }

    void CameraRotation()
    //switches the camera's rotation based on different points in the scene
    //allows movement of the camera getting it from input, applying the vector direction then set it across the delta time
    {

        if (!placement)
        {
            return;
        }
        else if (hover != null)
        {
            if (Hover.componentSelected || placement.deletingObject || hover.isTab)
                return;
        }

#if UNITY_STANDALONE
        transform.LookAt(cameras[selectedCamera]);
        //transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
        float rotationAmountY = -Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        float rotationAmountX = Input.GetAxisRaw("Vertical") * rotationSpeed * Time.deltaTime;
        float currentEulerAngleY = transform.rotation.eulerAngles.y;
        float currentEulerAngleX = transform.rotation.eulerAngles.x;
        if (rotationAmountY != 0)
        {
            if ((currentEulerAngleY + rotationAmountY) > maxRotationY)
            {
                //rotationAmountY = maxRotationY - currentEulerAngleY;
                rotationAmountY = 0;
            }
            else if (currentEulerAngleY + rotationAmountY < minRotationY)
            {
                //rotationAmountY = minRotationY - currentEulerAngleY;
                rotationAmountY = 0;
            }
            transform.RotateAround(cameras[0].position, transform.up, rotationAmountY);
            //transform.Rotate(Vector3.up, rotationAmountY);
        }
        if (rotationAmountX != 0)
        {
            //Debug.Log(currentEulerAngleX + rotationAmountX);
            if ((currentEulerAngleX + rotationAmountX) > maxRotationX && (currentEulerAngleX + rotationAmountX) < 180)
            {
                rotationAmountX = 0;
                //rotationAmountX = maxRotationX - currentEulerAngleX;
            }
            else if ((currentEulerAngleX + rotationAmountX < minRotationX) && (currentEulerAngleX + rotationAmountX > 180))
            {
                rotationAmountX = 0;
                //rotationAmountX = maxRotationX - currentEulerAngleX;
            }
            //transform.Rotate(Vector3.right, rotationAmountX);
            transform.RotateAround(cameras[0].position, transform.right, rotationAmountX);
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            transform.LookAt(cameras[selectedCamera]);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            //if the finger has moved
            //checks the difference between the intial touch position of the finger to the new finger position
            {
                Vector2 swipeDelta = Input.GetTouch(0).position - startPos;

                float rotationAmountY = swipeDelta.x / 100f * rotationSpeed * Time.deltaTime;
                float rotationAmountX = -swipeDelta.y / 100f * rotationSpeed * Time.deltaTime;
                float currentEulerAngleY = transform.rotation.eulerAngles.y;
                float currentEulerAngleX = transform.rotation.eulerAngles.x;
                if (rotationAmountY != 0)
                {
                    if ((currentEulerAngleY + rotationAmountY) > maxRotationY)
                    {
                        //rotationAmountY = maxRotationY - currentEulerAngleY;
                        rotationAmountY = 0;
                    }
                    else if (currentEulerAngleY + rotationAmountY < minRotationY)
                    {
                        //rotationAmountY = minRotationY - currentEulerAngleY;
                        rotationAmountY = 0;
                    }
                    transform.RotateAround(cameras[0].position, transform.up, rotationAmountY);
                    //transform.Rotate(Vector3.up, rotationAmountY);
                }
                if (rotationAmountX != 0)
                {
                    //Debug.Log(currentEulerAngleX + rotationAmountX);
                    if ((currentEulerAngleX + rotationAmountX) > maxRotationX && (currentEulerAngleX + rotationAmountX) < 180)
                    {
                        rotationAmountX = 0;
                        //rotationAmountX = maxRotationX - currentEulerAngleX;
                    }
                    else if ((currentEulerAngleX + rotationAmountX < minRotationX) && (currentEulerAngleX + rotationAmountX > 180))
                    {
                        rotationAmountX = 0;
                        //rotationAmountX = maxRotationX - currentEulerAngleX;
                    }
                    //transform.Rotate(Vector3.right, rotationAmountX);
                    transform.RotateAround(cameras[0].position, transform.right, rotationAmountX);
                }


                transform.RotateAround(cameras[0].position, transform.up, rotationAmountY);
                transform.RotateAround(cameras[0].position, transform.right, rotationAmountX);
                //transform.rotation = Quaternion.Euler(0, 0, rotationAmountXtemp);
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

    void ZoomCamera() 
    {
        if (!placement)
        {
            return;
        }
        else if (hover != null)
        {
            if (Hover.componentSelected || placement.deletingObject || hover.isTab)
                return;
        }
#if UNITY_STANDALONE
        if (Input.mouseScrollDelta.y != 0)
        {
            zoomAmount += -Input.mouseScrollDelta.y * sensitivity * 50f * Time.deltaTime;
            float zoom = Mathf.Clamp(zoomAmount, 20, 100);

            Camera.main.fieldOfView = zoom;
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (Physics.Raycast(Camera.main.ScreenPointToRay((Input.GetTouch(0).position + Input.GetTouch(1).position) / 2), out RaycastHit hit, Mathf.Infinity))
                {

                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                    transform.LookAt(hit.point);
                }
            }
            //else if (InputSystem.Instance.isStationary(2, deltaDistance) && deltaDistance != 0)
            //{
            //    return;
            //}
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(1).phase == TouchPhase.Stationary)
            {
                initialDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                deltaDistance = currentDistance - initialDistance;

                if (InputSystem.Instance.isStationary(2, initialDistance, deltaDistance))
                {
                    return;
                }

                zoomAmount += -deltaDistance * sensitivity * Time.deltaTime;
                if (zoomAmount > 100)
                {
                    zoomAmount = 100;
                }
                if (zoomAmount < 20)
                {
                    zoomAmount = 20;
                }
                float zoom = Mathf.Clamp(zoomAmount, 20, 100);
                Camera.main.fieldOfView = zoom;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                zoomAmount = Camera.main.fieldOfView;
            }
        }
#endif
        //else
        //{
        //    if (!moved)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, transform.forward, 100 * Time.deltaTime);
        //        if (Vector3.Distance(transform.position, cameras[0].position) < 60)
        //        {
        //            moved = true;
        //        }
        //    }
            
            //zooming = false;
        //}
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
            if (selectedCamera > 0)
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

    //void ClipCheck(Vector3 position)
    //{
    //    Ray ray1 = new (position, transform.forward);
    //    Ray ray2 = new(transform.position, transform.forward);
    //    if (Physics.Raycast(ray2, out RaycastHit hit2, maxZoom))
    //    {
    //        if (hit2.distance < zoomStopDistance)
    //        {
    //            transform.position = new(hit2.point.x - zoomStopDistance, hit2.point.y, hit2.point.z);
    //            return;
    //        }
    //    }
    //    if (Physics.Raycast(ray1, out RaycastHit hit1, maxZoom))
    //    {
    //        if (hit1.distance < zoomStopDistance)
    //        {
    //            transform.position = new Vector3(hit1.point.x - zoomStopDistance, hit1.point.y, hit1.point.z);
    //            return;
    //        }
    //    }
    //}
}
