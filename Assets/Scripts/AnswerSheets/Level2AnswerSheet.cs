using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level2AnswerSheet : MonoBehaviour
{
    public List<GameObject> connectionPoint1;
    public List<GameObject> connectionSubPoint1;
    public List<GameObject> connectionPoint2;
    public List<GameObject> connectionPoint3;
    public List<GameObject> connectionSubPoint3;
    public List<GameObject> connectionPoint4;

    public bool connectionCHWR;
    public bool connectionCHWS;
    public bool connectionCWS;
    public bool connectionCWR;

    public GameObject correctPanel;
    bool showPopUp;

    // Start is called before the first frame update
    void Start()
    {
        showPopUp = true;
        //Debug.Log(connectionPoint1.Count);
    }

    // Update is called once per frame
    void Update()
    {
        ConnectionCheck();
        AnswerCheck();
    }

    public void AnswerCheck()
    {
        if (showPopUp)
        {
            if (connectionCHWR && connectionCHWS && connectionCWS && connectionCWR)
            {
                correctPanel.SetActive(true);
                Debug.Log("Correct");
                showPopUp = false;
            }
        }
    }

    void ConnectionCheck()
    {
        var CHWR = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection 1");
        var CHWS = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection 2");
        var CWS = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection 3");
        var CWR = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection 4");

        if (CHWR.Count() == 2)
        {
            connectionCHWR = true;
        }
        else
        {
            connectionCHWR = false;
        }

        if (CHWS.Count() == 1)
        {
            connectionCHWS = true;
        }
        else
        {
            connectionCHWS = false;
        }

        if (CWS.Count() == 2)
        {
            connectionCWS = true;
        }
        else
        {
            connectionCWS = false;
        }

        if (CWR.Count() == 1)
        {
            connectionCWR = true;
        }
        else
        {
            connectionCWR = false;
        }
    }

    public bool ListComparison(List<GameObject> playerList)
    {
        if (playerList.Count == connectionPoint1.Count)
        {
            Debug.Log("checking 1");
            for (int i = 0; i < connectionPoint1.Count; i++)
            {
                if (connectionPoint1[i].transform.position == playerList[i].transform.position)
                {
                    if (i == connectionPoint1.Count - 1)
                    {
                        Debug.Log("Matches set 1");
                        return true;
                    }
                }
            }
        }
        
        if (playerList.Count == connectionSubPoint1.Count)
        {
            Debug.Log("checking s1");
            for (int i = 0; i < connectionSubPoint1.Count; i++)
            {
                if (connectionSubPoint1[i].transform.position == playerList[i].transform.position)
                {
                    if (i == connectionSubPoint1.Count - 1)
                    {
                        Debug.Log("Matches sub set 1");
                        return true;
                    }
                }
            }
        }
        
        if (playerList.Count == connectionPoint2.Count)
        {
            Debug.Log("checking 2");
            for (int i = 0; i < connectionPoint2.Count; i++)
            {
                if (connectionPoint2[i].transform.position == playerList[i].transform.position)
                {
                    if (i == connectionPoint2.Count - 1)
                    {
                        Debug.Log("Matches set 2");
                        return true;
                    }
                }
            }
        }
        
        if (playerList.Count == connectionPoint3.Count)
        {
            Debug.Log("checking 3");
            for (int i = 0; i < connectionPoint3.Count; i++)
            {
                if (connectionPoint3[i].transform.position == playerList[i].transform.position)
                {
                    if (i == connectionPoint3.Count - 1)
                    {
                        Debug.Log("Matches set 3");
                        return true;
                    }
                }
            }
        }
        
        if (playerList.Count == connectionSubPoint3.Count)
        {
            Debug.Log("checking s3");
            for (int i = 0; i < connectionSubPoint3.Count; i++)
            {
                if (connectionSubPoint3[i].transform.position == playerList[i].transform.position)
                {
                    if (i == connectionSubPoint3.Count - 1)
                    {
                        Debug.Log("Matches sub set 3");
                        return true;
                    }
                }
            }
        }
        
        if (playerList.Count == connectionPoint4.Count)
        {
            Debug.Log("checking 4");
            for (int i = 0; i < connectionPoint4.Count; i++)
            {
                if (connectionPoint4[i].transform.position == playerList[i].transform.position)
                {
                    Debug.Log("position mactches");

                    if (i == connectionPoint4.Count - 1)
                    {
                        Debug.Log("Matches set 4");
                        return true;
                    }
                }
                else
                {
                    Debug.Log("no position match");
                }
            }
        }

        return false;
    }

    //public void ListComparison(List<GameObject> playerList, List<GameObject> answerList)
    //{

    //}
}
