using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryBook : MonoBehaviour
{
    [Header("Theory Book")]
    public GameObject TheoryBookComponent;
    public GameObject TheoryBookPipes; 

    [Header("Pages")]
    public GameObject Comppage1;
    public GameObject Comppage2;
    public GameObject Comppage3;

    public GameObject Pipepage1;
    public GameObject Pipepage2;

    [Header("Poloroid Frame")]
    public GameObject frame;

    [Header("For Main Menu Use")]
    [SerializeField] private GameObject quitBtn;


    public void OpenTheoryBook()
    {
        if (quitBtn)
        { 
            quitBtn.SetActive(false);
        }

        TheoryBookComponent.SetActive(true);
    }

    public void CloseTheoryBook()
    {
        if (quitBtn)
        {
            quitBtn.SetActive(true);
        }

        TheoryBookComponent.SetActive(false);
        TheoryBookPipes.SetActive(false);
    }

    public void ComponentPage1()
    {
        Comppage1.SetActive(true);
        Comppage2.SetActive(false);
        Comppage3.SetActive(false);
        frame.SetActive(true);
    }

    public void ComponentPage2() 
    {
        Comppage1.SetActive(false);
        Comppage2.SetActive(true);
        Comppage3.SetActive(false);
        frame.SetActive(true);
    }

    public void ComponentPage3()
    {
        Comppage1.SetActive(false);
        Comppage2.SetActive(false);
        Comppage3.SetActive(true);
        frame.SetActive(false);
    }

    public void PipePage1()
    {
        Pipepage1.SetActive(true);
        Pipepage2.SetActive(false);

    }

    public void PipePage2()
    {
        Pipepage1.SetActive(false);
        Pipepage2.SetActive(true);

    }
    public void ComponentBook()
    {
        TheoryBookPipes.SetActive(false);
        TheoryBookComponent.SetActive(true);
    }
    public void PipeBook()
    {
        TheoryBookComponent.SetActive(false);
        TheoryBookPipes.SetActive(true);
    }
}
