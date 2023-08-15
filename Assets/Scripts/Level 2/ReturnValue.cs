using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnValue : MonoBehaviour
{
    public int indexValue;
    public SelectedComponent selectedComponentBtn;
    public Connection connection;
    public bool pressedBtn;
    Color greenish = new Color(0, 255, 0, 255);
    Color whiter = new Color(255, 255, 255, 143);

    private void Start()
    {
        connection = FindObjectOfType<Connection>();
        pressedBtn = false;
        indexValue = 0;


        foreach (Button btn in transform.GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(delegate { ReturnConnectionPoint(); });
        }
        selectedComponentBtn = connection.selectedComponent;

        foreach (GameObject t in selectedComponentBtn.selectedTransform)
        {
            var mesh = t.GetComponent<MeshRenderer>().sharedMaterial;

            if (mesh == connection.selectionMat)
            {
                int index = selectedComponentBtn.IndexReturning(t);
                if (index == int.Parse(GetComponentInChildren<Button>().gameObject.name))
                {
                    gameObject.transform.GetChild(index-1).GetComponent<Image>().color = greenish;
                }
            }
        }
    }

    public void ReturnConnectionPoint()
    {
        
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;

        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color != greenish)
        {
            //pressedBtn = true;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = greenish;
            indexValue = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color != whiter)
        {
            Debug.Log(pressedBtn);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = whiter;
            indexValue = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        scriptRef();
    }

    public int ReturnIndex() 
    {
        connection.valueReturnBtn = this;
        return indexValue;
    }

    private void scriptRef() 
    {
        if (pressedBtn)
        {
            connection.valueReturnBtn = this;
        }
    }

    
}
