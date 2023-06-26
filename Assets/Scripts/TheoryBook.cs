using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryBook : MonoBehaviour
{
    [Header("Theory Book")]
    public GameObject theoryBook;
    public GameObject TheoryBookPipes; 

    [Header("Pages")]
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    [Header("Poloroid Frame")]
    public GameObject frame;


    public void OpenTheoryBook()
    {
        theoryBook.SetActive(true);
    }

    public void CloseTheoryBook()
    {
        theoryBook.SetActive(false);
    }

    public void Page1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        frame.SetActive(true);
    }

    public void Page2() 
    {
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
        frame.SetActive(true);
    }

    public void Page3()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
        frame.SetActive(false);
    }

    public void PipeBook() 
    {
        theoryBook.SetActive(false);
        TheoryBookPipes.SetActive(true);
    }

    public void ComponentBook()
    {
        theoryBook.SetActive(true);
        TheoryBookPipes.SetActive(false);
    }

}
