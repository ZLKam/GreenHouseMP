using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public GameObject component;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Component")
        {
            other.transform.position = transform.position;
        }
    }
}
