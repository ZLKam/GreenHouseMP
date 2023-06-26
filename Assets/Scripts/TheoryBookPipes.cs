using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryBookPipes : MonoBehaviour
{
    [Header("Theory Book Pipe")]
    public GameObject theoryBook;

    [Header("Pages")]
    public GameObject page1;
    public GameObject page2;  


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

    }

    public void Page2()
    {
        page1.SetActive(false);
        page2.SetActive(true);

    }


}
