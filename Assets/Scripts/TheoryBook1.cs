using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TheoryBook1 : MonoBehaviour
{

    public Sprite blankSprite;
    public GameObject previousBtn, nextBtn;
    public TextMeshProUGUI title1, title2;
    public TextMeshProUGUI description1, description2;
    public GameObject template;

    [Header("Theory Book")]
    public GameObject TheoryBookComponent;
    public GameObject TheoryBookPipes;

    [Header("Pages")]
    [SerializeField]
    private List<Sprite> componentSprite = new List<Sprite>();
    [SerializeField]
    private List<string> componentName = new List<string>();
    [SerializeField]
    private List<string> componentDescription = new List<string>();

    [SerializeField]
    private Image image1, image2;

    private int i;

    public GameObject Comppage1;
    public GameObject Comppage2;
    public GameObject Comppage3;

    public GameObject Pipepage1;
    public GameObject Pipepage2;

    [Header("Poloroid Frame")]
    public GameObject frame;

    [Header("For Main Menu Use")]
    [SerializeField] private GameObject quitBtn;

    private void Start()
    {
        image1.GetComponent<Image>().sprite = componentSprite[i];
        image2.GetComponent<Image>().sprite = componentSprite[i + 1];
    }

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
    }

    public void NextPage()
    {
        if (i+2 > componentSprite.Count)
            return;

        if (i + 2 < componentSprite.Count)
        {
            i += 2;
            if (i + 1 < componentSprite.Count)
            {
                image2.GetComponent<Image>().sprite = componentSprite[i + 1];
                title2.text = componentName[i + 1];
            }
            else 
            {
                template.SetActive(false);
                image2.gameObject.SetActive(false);
                title2.gameObject.SetActive(false);
                description2.gameObject.SetActive(false);
                nextBtn.SetActive(false);
            }
        }
        image1.GetComponent<Image>().sprite = componentSprite[i];
        title1.text = componentName[i];
        previousBtn.SetActive(true);
    }

    public void PreviousPage() 
    {
        if (i - 2 > -1) 
        {
            nextBtn.SetActive(true);
            template.SetActive(true);
            title2.gameObject.SetActive(true);
            description2.gameObject.SetActive(true);
            image2.gameObject.SetActive(true);
            i -= 2;
            image1.GetComponent<Image>().sprite = componentSprite[i];
            image2.GetComponent<Image>().sprite = componentSprite[i + 1];
            title1.text = componentName[i];
            title2.text = componentName[i + 1];
            if (i - 2 < 0) 
            {
                previousBtn.SetActive(false);
            }
        }
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
