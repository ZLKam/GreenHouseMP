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

    Transform[] CHWR;
    Transform[] CHWRsub;
    Transform[] CHWS;
    Transform[] CWS;
    Transform[] CWSsub;
    Transform[] CWR;

    //public Transform[] CHWRans;
    //public Transform[] CHWSans;
    //public Transform[] CWSans;
    //public Transform[] CWRans;
    public Transform[] pipes;

    // Start is called before the first frame update
    void Start()
    {
        showPopUp = true;
        //Debug.Log(connectionPoint1.Count);

        CHWR = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Point 1").ToArray();
        CHWRsub = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Sub Point 1").ToArray();
        CHWS = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Point 2").ToArray();
        CWS = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Point 3").ToArray();
        CHWRsub = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Sub Point 3").ToArray();
        CWR = Resources.FindObjectsOfTypeAll<Transform>().Where(obj => obj.name == "Connection Point 4").ToArray();
        Debug.Log(CHWR.Count());
        //Debug.Log(CHWS.Count());
        //Debug.Log(CWS.Count());
        //Debug.Log(CWR.Count());
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
        }
    }

    void ConnectionCheck()
    {

        if (CHWR.Count() == 2)
        {
            connectionCHWR = true;
        }
        else
        {
            connectionCHWR = false;
            return;
        }
        
        if (CHWS.Count() == 1)
        {
            connectionCHWS = true;
        }
        else
        {
            connectionCHWS = false;
            return;
        }

        if (CWS.Count() == 2)
        {
            connectionCWS = true;
        }
        else
        {
            connectionCWS = false;
            return;
        }

        if (CWR.Count() == 1)
        {
            connectionCWR = true;
        }
        else
        {
            connectionCWR = false;
            return;
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
            for (int i = 0; i < connectionSubPoint1.Count; i++) //5,5
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
    public void ConnectionCheck(List<Transform> ans)
    {
        if (ans[0] == pipes[0])
        {
            foreach (Transform t in ans)
            {
                if (!CHWR.Contains(t) || !CHWRsub.Contains(t))
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
            foreach (Transform t in ans)
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
            foreach (Transform t in ans)
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
            foreach (Transform t in ans)
            {
                if (!CWS.Contains(t) || !CWSsub.Contains(t))
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
