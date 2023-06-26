using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tutorial : MonoBehaviour
{
    public float timer;
    public GameObject brighten;
    public List<GameObject> popUpList;
    public List<GameObject> selection;
    public List<GameObject> elements;
    public GameObject connection;
    public RectTransform hover;
    public int index;
    Fade brightness;
    Image bright;
    bool show;
    bool check;
    bool moveWheel;
    bool checkPipe;

    ParticleFlow pipe;

    // Start is called before the first frame update
    void Start()
    {
        brightness = brighten.GetComponent<Fade>();
        bright = brighten.GetComponent<Image>();

        StartCoroutine(StartPopUp());
    }

    // Update is called once per frame
    void Update()
    {

        if (popUpList[index].name == "InventoryPopUp (1)")
        {
            StartCoroutine(ComponentTutorial());
        }

        if(popUpList[index].name == "PipesPopUp (1)")
        {
            StartCoroutine(ConnectionTutorial());
        }

        if (popUpList[index].name == "IndicatorPopUp")
        {
            BringToFront("Level Indicator");
        }
        else
        {
            SendToBack("Level Indicator");
        }

        if (popUpList[index].name == "TheoryPopUp")
        {
            BringToFront("TheoryBtn");
        }
        else
        {
            SendToBack("TheoryBtn");
        }

        if (popUpList[index].name == "SettingPopUp")
        {
            BringToFront("SettingsBtn");
        }
        else
        {
            SendToBack("SettingsBtn");
        }

        if (moveWheel)
        {
            if(hover.transform.localPosition.x > -786)
            {
                hover.transform.position -= new Vector3(125, 0f, 0f) * Time.deltaTime * 5;
            }
        }
    }

    IEnumerator StartPopUp()
    {
        yield return new WaitForSeconds(timer);

        bright.enabled = true;
        brightness.darken = true;

        yield return new WaitForSeconds(timer / 2);

        popUpList[0].SetActive(true);
    }

    IEnumerator ComponentTutorial()
    {
        brightness.darken = false;

        yield return new WaitForSeconds(timer / 2);

        popUpList[index].SetActive(true);
    }

    IEnumerator ConnectionTutorial()
    {
        foreach (GameObject select in selection)
        {
            select.SetActive(false);
        }
        connection.SetActive(true);
        moveWheel = true;
        brightness.darken = false;

        yield return new WaitForSeconds(timer / 2);

        popUpList[index].SetActive(true);
    }

    IEnumerator Continue()
    {
        brightness.darken = true;
        
        yield return new WaitForSeconds(timer / 2);

        popUpList[index].SetActive(false);
        index++;
    }

    public void CheckPlacement()
    {
        if (selection.All(i => i.transform.childCount > 0))
        {
            check = true;

            popUpList[index].transform.Find("Tick").gameObject.SetActive(true);

            StartCoroutine(Continue());
        }
    }

    public void CheckConnection()
    {
        pipe = FindObjectOfType<ParticleFlow>();

        if(pipe != null)
        {
            checkPipe = true;

            popUpList[index].transform.Find("Tick").gameObject.SetActive(true);

            StartCoroutine(Continue());
        }
    }

    public void ResetPlacement()
    {
        if(selection.All(i => i.transform.childCount > 0))
        {
            foreach(GameObject select in selection)
            {
                select.GetComponent<Renderer>().enabled = true;
                select.GetComponent<BoxCollider>().enabled = true;
                Destroy(select.transform.GetChild(0).gameObject);
            }
        }
    }

    void ResetPipe()
    {
        if(pipe != null)
        {
            Destroy(pipe.gameObject);
        }
    }

    public void NextPopUp()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        index++;
        for(int i = 0; i < popUpList.Count; i++)
        {
            if (i == index)
            {
                popUpList[i].gameObject.SetActive(true);
            }
            else
            {
                popUpList[i].GetComponent<PopUp>().pop = true;
                popUpList[i].gameObject.SetActive(false);
            }
        }
    }

    public void PreviousPopUp()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        index--;

        if (popUpList[index].name == "InventoryPopUp (1)" && check)
        {
            check = false;
            popUpList[index].transform.Find("Tick").gameObject.SetActive(false);

            ResetPlacement();
        }

        if (popUpList[index].name == "PipesPopUp (1)" && checkPipe)
        {
            checkPipe = false;
            popUpList[index].transform.Find("Tick").gameObject.SetActive(false);

            ResetPipe();
        }

        for (int i = 0; i < popUpList.Count; i++)
        {
            if (i == index)
            {
                popUpList[i].gameObject.SetActive(true);
            }
            else
            {
                popUpList[i].GetComponent<PopUp>().pop = true;
                popUpList[i].gameObject.SetActive(false);
            }
        }
    }

    public void BringToFront(string name)
    {
        foreach (GameObject element in elements)
        {
            if(element.name == name)
            {
                element.transform.SetSiblingIndex(popUpList.Count);
            }
        }
    }

    public void SendToBack(string name)
    {
        foreach (GameObject element in elements)
        {
            if (element.name == name)
            {
                element.transform.SetSiblingIndex(0);
            }
        }
    }
}
