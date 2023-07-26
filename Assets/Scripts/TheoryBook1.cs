using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TheoryBook1 : MonoBehaviour
{

    public Sprite blankSprite;
    public GameObject previousBtn, nextBtn;
    public TextMeshProUGUI title1, title2;
    public TextMeshProUGUI description1, description2;
    public GameObject template;
    [SerializeField]
    private TextAsset theoryBookFormat;
    public string[] test;
    public string[] test2;
    public string[] test4;
    public List<string> componentsLines = new List<string>();
    public GameObject componentBtn, pipeBtn;

    private int currentLine;

    public List<string> testList = new List<string>();

    [Header("Theory Book")]
    public GameObject TheoryBookComponent;
    public GameObject TheoryBookPipes;

    [Header("Pages")]
    [SerializeField]
    private List<Sprite> componentSprite = new List<Sprite>();

    [SerializeField]
    private Image image1, image2;

    private int i;

    [Header("Poloroid Frame")]
    public GameObject frame;

    [Header("For Main Menu Use")]
    [SerializeField] private GameObject quitBtn;

    public int componentStartLine = 0;
    public int componentEndLine = 0;

    public bool component;

    private void Start()
    {
        image1.GetComponent<Image>().sprite = componentSprite[i];
        image2.GetComponent<Image>().sprite = componentSprite[i + 1];
        string[] lines = theoryBookFormat.text.Split('\n');
        testList = lines.ToList();

        Initialize();


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
                //title2.text = componentName[i + 1];
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
        //title1.text = componentName[i];
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
            //title1.text = componentName[i];
            //title2.text = componentName[i + 1];
            if (i - 2 < 0) 
            {
                previousBtn.SetActive(false);
            }
        }
    }

    public void NextPageTest() 
    {
        currentLine++;

        if (currentLine + 1 > componentsLines.Count - 1)
        {
            nextBtn.SetActive(false);
        }
        if (!previousBtn.activeInHierarchy)
            previousBtn.SetActive(true);

        test4 = componentsLines[currentLine].Split(" & ");

        test = test4[0].Split(" | ");
        test2 = test4[1].Split(" | ");

        textAssign(test, title1, description1);
        textAssign(test2, title2, description2);
    }

    public void PreviousPageTest() 
    {
        currentLine--;
        if (currentLine - 1 < 0)
        {
            previousBtn.SetActive(false);
        }
        if (!nextBtn.activeInHierarchy)
            nextBtn.SetActive(true);


        test4 = componentsLines[currentLine].Split(" & ");

        test = test4[0].Split(" | ");
        test2 = test4[1].Split(" | ");

        textAssign(test, title1, description1);
        textAssign(test2, title2, description2);
    }

    private void textAssign(string[] array, TextMeshProUGUI title, TextMeshProUGUI description) 
    {
        foreach (string item in array)
        {
            if (item.Contains("ComponentName"))
            {
                int test2 = item.IndexOf(":");
                title.text = item.Substring(test2 + 1);

            }
            else if (item.Contains("ComponentDescription"))
            {
                int test2 = item.IndexOf(":");
                description.text = item.Substring(test2 + 1);
            }
        }
    }

    private void Initialize() 
    {
        componentsLines.Clear();
        currentLine = 0;
        for (int i = 0; i < testList.Count; i++)
        {
            if (component)
            {
                if (testList[i].StartsWith("Components"))
                {
                    componentStartLine = i + 1;
                }
                if (testList[i].StartsWith("EndComponents"))
                {
                    componentEndLine = i;
                }
            }
            else if (!component)
            {
                if (testList[i].StartsWith("Pipe"))
                {
                    componentStartLine = i + 1;
                }
                if (testList[i].StartsWith("EndPipes"))
                {
                    componentEndLine = i;
                }
            }
        }
        for (int i = componentStartLine; i < componentEndLine; i++)
        {
            componentsLines.Add(testList[i]);
        }

        test4 = componentsLines[currentLine].Split(" & ");

        test = test4[0].Split(" | ");
        test2 = test4[1].Split(" | ");

        textAssign(test, title1, description1);
        textAssign(test2, title2, description2);
    }

    public void ComponentBook()
    {
        TheoryBookComponent.SetActive(true);
        TheoryBookPipes.SetActive(false);
        componentBtn.SetActive(false);
        pipeBtn.SetActive(true);
        component = true;
        Initialize();
    }
    public void PipeBook()
    {
        TheoryBookComponent.SetActive(false);
        TheoryBookPipes.SetActive(true);
        componentBtn.SetActive(true);
        pipeBtn.SetActive(false);
        component = false;
        Initialize();
    }
}
