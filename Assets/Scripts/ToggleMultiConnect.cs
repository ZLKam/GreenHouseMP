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
    }

    public void UpdateText() 
    {
        connectionCountText.text = "Connection Points Left: " + (connection.multiConnectLimit - connection.multiPoints.Count).ToString();
    }
}
