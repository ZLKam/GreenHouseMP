using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCamera : MonoBehaviour
{
    [SerializeField]
    private Camera cameraToFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = cameraToFollow.transform.position;
        gameObject.transform.rotation = cameraToFollow.transform.rotation;
    }
}
