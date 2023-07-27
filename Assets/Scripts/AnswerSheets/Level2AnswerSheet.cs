using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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
    public GameObject wrongPanel;
    public static bool showPopUp;

    GameObject[] CHWR;
    GameObject[] CHWS;
    GameObject[] CWS;
    GameObject[] CWR;

    GameObject[] CHWRans;
    GameObject[] CHWSans;
    GameObject[] CWSans;
    GameObject[] CWRans;

    public TextMeshProUGUI text;

    //public Transform[] CHWRans;
    //public Transform[] CHWSans;
    //public Transform[] CWSans;
    //public Transform[] CWRans;
    public Transform[] pipes;

    // Start is called before the first frame update
    void Start()
    {
        CHWSans = CHWRans = CWSans = CWRans = null;
        showPopUp = true;
        //Debug.Log(connectionPoint1.Count);

        CHWR = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection Point 1").ToArray();
        CHWS = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection Point 2").ToArray();
        CWS = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection Point 3").ToArray();
        CWR = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Connection Point 4").ToArray();
        //Debug.Log(CHWR.Count());
        //Debug.Log(CHWS.Count());
        //Debug.Log(CWS.Count());
        //Debug.Log(CWR.Count());
    }

    private void Update()
    {
    }

    public void AnswerCheck()
    {
        ConnectionCheck();
        if (showPopUp)
        {
            if (connectionCHWR && connectionCHWS && connectionCWS && connectionCWR)
            {
                correctPanel.SetActive(true);
                Debug.Log("Correct");
                showPopUp = false;
            }
            else
            {
                wrongPanel.SetActive(true);
                Debug.Log("wrong");
                showPopUp = false;
                CHWSans = CHWRans = CWSans = CWRans = null;
            }
        }
    }

    void ConnectionCheck()
    {
        CHWRans = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pipe CHWR(Clone)" || obj.name == "Connection 1").ToArray();
        CHWSans = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pipe CHWS(Clone)" || obj.name == "Connection 2").ToArray();
        CWSans = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pipe CWS(Clone)" || obj.name == "Connection 3").ToArray();
        CWRans = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pipe CWR(Clone)" || obj.name == "Connection 4").ToArray();

        Debug.Log(CWRans.Count());




        if (CHWRans.Count() == 2)
        {
            connectionCHWR = true;
            //Debug.Log("CHWR Correct");
        }
        else
        {
            connectionCHWR = false;
        }
        
        if (CHWSans.Count() == 1)
        {
            connectionCHWS = true;
            //Debug.Log("CHWS Correct");
        }
        else
        {
            connectionCHWS = false;
        }

        if (CWSans.Count() == 2)
        {
            connectionCWS = true;
            //Debug.Log("CWS Correct");
        }
        else
        {
            connectionCWS = false;
        }

        if (CWRans.Count() == 1)
        {
            connectionCWR = true;
            //Debug.Log("CWR Correct");
        }
        else
        {
            connectionCWR = false;
        }
    }

    //void ConnectionCheckv2(List<Transform> ans1, List<Transform> ans2, List<Transform> ans3, List<Transform> ans4)
    //{
    //    foreach (Transform t in ans1)
    //    {
    //        if (!CHWRans.Contains(t))
    //        {
    //            connectionCHWR = false;
    //        }
    //    }

    //    foreach (Transform t in ans2)
    //    {
    //        if (!CHWSans.Contains(t))
    //        {
    //            connectionCHWS = false;
    //        }
    //    }

    //    foreach (Transform t in ans3)
    //    {
    //        if (!CWRans.Contains(t))
    //        {
    //            connectionCWR = false;
    //        }
    //    }
    //    foreach (Transform t in ans4)
    //    {
    //        if (!CWSans.Contains(t))
    //        {
    //            connectionCWS = false;
    //        }
    //    }
    //    else { }
    //}

    public bool ListComparison(List<GameObject> playerList)
    {
        if (playerList.Count == connectionPoint1.Count) //Connection 1 has 7 gameobjects,in fame 7, in script 5, answer 2
        {
            Debug.Log("checking 1");
            for (int i = 0; i < connectionPoint1.Count; i++)
            {
                if (connectionPoint1[i].transform.position == playerList[i].transform.position) //(connectionPoint1[i].transform.position == playerList[i].transform.position) (playerList.Contains(connectionSubPoint3[i]))
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
            for (int i = 0; i < connectionSubPoint1.Count; i++) //5,5
            {
                if (connectionSubPoint1[i].transform.position == playerList[i].transform.position) //(connectionSubPoint1[i].transform.position == playerList[i].transform.position) (playerList.Contains(connectionSubPoint3[i]))
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
                if (connectionPoint2[i].transform.position == playerList[i].transform.position) //(!playerList.Contains(connectionPoint2[i])) (connectionPoint2[i].transform.position == playerList[i].transform.position)
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
                if (connectionPoint3[i].transform.position == playerList[i].transform.position) //(connectionPoint3[i].transform.position == playerList[i].transform.position)  (playerList.Contains(connectionPoint3[i]))
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
                if (connectionSubPoint3[i].transform.position == playerList[i].transform.position) //(connectionSubPoint3[i].transform.position == playerList[i].transform.position) (!playerList.Contains(connectionSubPoint3[i]))
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
                if (connectionPoint4[i].transform.position == playerList[i].transform.position) //(connectionPoint4[i].transform.position == playerList[i].transform.position) (!playerList.Contains(connectionPoint4[i]))
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
    public void ConnectionCheck(List<GameObject> ans)
    {
        if (ans[0] == pipes[0])
        {
            foreach (GameObject t in ans)
            {
                if (!CHWR.Contains(t))
                {
                    connectionCHWR = false;
                }
                else
                {
                    connectionCHWR = true;
                }
            }
        }
        if (ans[0] == pipes[1])
        {
            //CHWS Check
            foreach (GameObject t in ans)
            {
                if (!CHWS.Contains(t))
                {
                    connectionCHWS = false;
                }
                else
                {
                    connectionCHWS = true;
                }
            }
        }
        if (ans[0] == pipes[2])
        {
            //CWR Check
            foreach (GameObject t in ans)
            {
                if (!CWR.Contains(t) )
                {
                    connectionCWR = false;
                }
                else
                {
                    connectionCWR = true;
                }
            }
        }
        if (ans[0] == pipes[3])
        {
            //CWS Check
            foreach (GameObject t in ans)
            {
                if (!CWS.Contains(t))
                {
                    connectionCWS = false;
                }
                else
                {
                    connectionCWS = true;
                }
            }
        }
    }
}
