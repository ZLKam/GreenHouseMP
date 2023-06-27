using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    public void OnToggle()
    {
        FindObjectOfType<Connection>().multiConnect = !FindObjectOfType<Connection>().multiConnect;
    }
}
