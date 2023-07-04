using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placing : MonoBehaviour
{
    public GameObject ahu;
    public ComponentWheelController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ComponentWheelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "ahu" && Input.GetButtonDown("Fire1") && controller.ahuCheck == true)
        {
            ahu.transform.localScale += new Vector3(10, 10, 10);
            Instantiate(ahu, transform.position, Quaternion.Euler(0f, 90f, 0f));
            gameObject.SetActive(false);
            
        }
        
    }
}
