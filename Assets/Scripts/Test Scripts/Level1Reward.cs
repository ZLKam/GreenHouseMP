using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1Reward : MonoBehaviour
{

    public GameObject instantiatePoint;
    public Camera cameraToTurnOn;
    public LayerMask mask;

    public GameObject testObjectToSpawn;
    public GameObject Panel;
    public TextMeshProUGUI textest;


    // Start is called before the first frame update
    void Start()
    {
        cameraToTurnOn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        testCamera();
    }

    private void testCamera()
    {
        Ray ray = new();
        if (Input.GetMouseButtonDown(0))
        {
            if (!cameraToTurnOn.gameObject.activeSelf)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = cameraToTurnOn.ScreenPointToRay(Input.mousePosition);
            }
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {

            cameraToTurnOn.gameObject.SetActive(true);
            if (instantiatePoint.transform.childCount < 1)
            {
                GameObject temp = Instantiate(testObjectToSpawn, instantiatePoint.transform.position, Quaternion.identity);
                foreach (Transform child in temp.transform)
                {
                    child.gameObject.layer = 11;
                }
                temp.transform.parent = instantiatePoint.transform;
            }
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //gets the previous touch position and checks the difference between the new position before applying that value as the rotation of the object
                instantiatePoint.transform.Rotate(Vector3.up, Input.GetTouch(0).deltaPosition.x, Space.World);
            }
        }



    }

    public void test()
    {
        if (!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
            textest.text = "This is information1";
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
