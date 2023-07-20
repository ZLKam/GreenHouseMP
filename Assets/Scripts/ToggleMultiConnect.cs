using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleMultiConnect : MonoBehaviour
{
    private Connection connection;

    [SerializeField]
    private GameObject connectionCountPanel;
    public TextMeshProUGUI connectionCountText;

    private void Start()
    {
        connection = FindObjectOfType<Connection>();
    }

    public void OnToggle()
    {
        connection.multiConnect = !connection.multiConnect;
        connectionCountPanel.SetActive(connection.multiConnect);
        connectionCountText.text = "Connection Points Left: " + "-";
    }

    public void UpdateText() 
    {
        if (connection.multiPoints.Count <= 0)
        {
            connectionCountText.text = "Connection Points Left: " + "-";
        }
        else
        {
            connectionCountText.text = "Connection Points Left: " + (connection.multiConnectLimit - connection.multiPoints.Count).ToString();
        }
    }
}
