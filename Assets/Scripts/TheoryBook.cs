using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TheoryBook : MonoBehaviour
//Uses TheoryBookFormat.txt to process text info to place into the theorybook
//make sure there is always an image for both the left and right pages before adding a new page
{

    private int currentLine;
    private int currentImage;

    private List<string> theoryBookList = new List<string>();
    private List<string> componentsLines = new List<string>();

    private int componentStartLine = 0;
    private int componentEndLine = 0;

    #region TextFile
    [Header("Theory Book Text Reference")]
    [Tooltip("Found at Resources/TextFiles")]
    [SerializeField]
    private TextAsset theoryBookFormat;
    #endregion

    #region Theory Book Components
    [Header("Theory Book")]
    public GameObject TheoryBookComponent;
    public GameObject TheoryBookPipes;
    public GameObject TheoryBookDiagrams;
    public GameObject TheoryBookTemplate;

    public GameObject previousBtn, nextBtn;

    public GameObject componentBtn, pipeBtn, diagramBtn;
    public TextMeshProUGUI leftHeader, rightHeader;
    #endregion

    #region Page Components
    [Header("Pages")]
    [SerializeField]
    private List<Sprite> componentSprite = new List<Sprite>();
    [SerializeField]
    private List<Sprite> pipeSprite = new List<Sprite>();
    [SerializeField]
    private List<Sprite> diagramSprite = new List<Sprite>();

    public TextMeshProUGUI leftTitle, rightTitle;
    public TextMeshProUGUI leftDescription, rightDescription;
    public Image leftIamge, rightImage;

    private string[] leftArray;
    private string[] rightArray;
    private string[] leftRightArray;
    #endregion

    [Header("Poloroid Frame")]
    public GameObject leftFrame;

    [Header("For Main Menu Use")]
    [SerializeField] private GameObject quitBtn;

    private void Start()
    {
        string[] lines = theoryBookFormat.text.Split('\n');
        theoryBookList = lines.ToList();

        Initialize();
    }

    public void OpenTheoryBook()
    //open theory book
    {
        if (quitBtn)
        //closes the quit button that is in main menu
        { 
            quitBtn.SetActive(false);
        }

        TheoryBookTemplate.SetActive(true);
    }

    public void CloseTheoryBook()
    //close theorybook
    {
        if (quitBtn)
        //turns back on the quit button in main menu
        {
            quitBtn.SetActive(true);
        }

        //TheoryBookComponent.SetActive(false);
        //TheoryBookPipes.SetActive(false);
        TheoryBookTemplate.SetActive(false);
    }

    public void NextPage() 
    //updates the name, description and image when incrementing upwards in pages
    {
        currentLine++;
        currentImage += 2;

        if (currentLine + 1 > componentsLines.Count - 1)
        {
            nextBtn.SetActive(false);
        }
        if (!previousBtn.activeInHierarchy)
            previousBtn.SetActive(true);

        leftRightArray = componentsLines[currentLine].Split(" & ");

        leftArray = leftRightArray[0].Split(" | ");
        rightArray = leftRightArray[1].Split(" | ");

        TextAssign(leftArray, leftTitle, leftDescription);
        TextAssign(rightArray, rightTitle, rightDescription);

        if (TheoryBookComponent.activeSelf)
        {
            ImageAssign(leftIamge, componentSprite, currentImage);
            if (currentImage + 2 <= componentSprite.Count)
            {
                ImageAssign(rightImage, componentSprite, currentImage + 1);
            }
            else 
            {
                rightHeader.text = "";
                leftFrame.SetActive(false);
                rightImage.gameObject.SetActive(false);
            }
        }
        else if(TheoryBookPipes.activeSelf)
        {
            ImageAssign(leftIamge, pipeSprite, currentImage);
            if (currentImage + 2 <= pipeSprite.Count)
            {
                ImageAssign(rightImage, pipeSprite, currentImage + 1);
            }
            else
            {
                rightHeader.text = "";
                leftFrame.SetActive(false);
                rightImage.gameObject.SetActive(false);
            }
        }
        else if (TheoryBookDiagrams.activeSelf)
        {
            ImageAssign(leftIamge, diagramSprite, currentImage);
            if (currentImage + 2 <= pipeSprite.Count)
            {
                ImageAssign(rightImage, diagramSprite, currentImage + 1);
            }
            else
            {
                rightHeader.text = "";
                leftFrame.SetActive(false);
                rightImage.gameObject.SetActive(false);
            }
        }
    }

    public void PreviousPage() 
    //updates the name, description and image when incrementing downwards in pages
    {
        currentLine--;
        currentImage -= 2;

        if (currentLine - 1 < 0)
        {
            previousBtn.SetActive(false);
        }
        if (!nextBtn.activeInHierarchy)
        {
            nextBtn.SetActive(true);
            leftFrame.SetActive(true);
            rightImage.gameObject.SetActive(true);
        }

        leftRightArray = componentsLines[currentLine].Split(" & ");

        leftArray = leftRightArray[0].Split(" | ");
        rightArray = leftRightArray[1].Split(" | ");

        TextAssign(leftArray, leftTitle, leftDescription);
        TextAssign(rightArray, rightTitle, rightDescription);

        if (TheoryBookComponent.activeSelf)
        {
            rightHeader.text = "COMPONENTS";
            ImageAssign(leftIamge, componentSprite, currentImage);
            ImageAssign(rightImage, componentSprite, currentImage + 1);
        }
        else if (TheoryBookPipes.activeSelf)
        {
            rightHeader.text = "PIPES";
            ImageAssign(leftIamge, pipeSprite, currentImage);
            ImageAssign(rightImage, pipeSprite, currentImage + 1);
        }
        else if (TheoryBookDiagrams.activeSelf) 
        {
            rightHeader.text = "DIAGRAMS";
            ImageAssign(leftIamge, diagramSprite, currentImage);
            ImageAssign(rightImage, diagramSprite, currentImage + 1);
        }
    }

    private void TextAssign(string[] array, TextMeshProUGUI title, TextMeshProUGUI description) 
    //assigns the text bassed on specific checkpoints in the array
    {
        foreach (string item in array)
        {
            if (item.Contains("ComponentName"))
            {
                int index = item.IndexOf(":");
                title.text = item.Substring(index + 1);

            }
            else if (item.Contains("ComponentDescription"))
            {
                int index = item.IndexOf(":");
                description.text = item.Substring(index + 1);
            }
        }
    }

    private void ImageAssign(Image image, List<Sprite> spriteList, int index) 
    //assings images based on paramteres
    {
        image.GetComponent<Image>().sprite = spriteList[index];
    }

    private void Initialize() 
    //for initializing the theorybook whenever changing scenes or changing books
    {
        componentsLines.Clear();
        currentLine = 0;
        currentImage = 0;
        previousBtn.SetActive(false);
        nextBtn.SetActive(true);
        leftFrame.SetActive(true);
        rightImage.gameObject.SetActive(true);

        if (TheoryBookComponent.activeSelf)
        {
            ComponentLinesAppend("Components", "EndComponents");
        }
        else if (TheoryBookPipes.activeSelf)
        {
            ComponentLinesAppend("Pipes", "EndPipes");
        }
        else if (TheoryBookDiagrams.activeSelf) 
        {
            ComponentLinesAppend("Diagrams", "EndDiagrams");
        }

        leftRightArray = componentsLines[currentLine].Split(" & ");

        leftArray = leftRightArray[0].Split(" | ");
        rightArray = leftRightArray[1].Split(" | ");

        TextAssign(leftArray, leftTitle, leftDescription);
        TextAssign(rightArray, rightTitle, rightDescription);

        if (TheoryBookComponent.activeSelf)
        {
            leftHeader.text = rightHeader.text = "COMPONENTS";
            ImageAssign(leftIamge, componentSprite, currentImage);
            ImageAssign(rightImage, componentSprite, currentImage + 1);
        }
        else if(TheoryBookPipes.activeSelf)
        {
            leftHeader.text = rightHeader.text = "PIPES";
            ImageAssign(leftIamge, pipeSprite, currentImage);
            ImageAssign(rightImage, pipeSprite, currentImage + 1);
        }
        else if (TheoryBookDiagrams.activeSelf)
        {
            leftHeader.text = rightHeader.text = "DIAGRAMS";
            ImageAssign(leftIamge, diagramSprite, currentImage);
            ImageAssign(rightImage, diagramSprite, currentImage + 1);
        }
    }

    private void ComponentLinesAppend(string startString, string endString)
    //changes which part of the text file to take based on the text that seperates the segments
    {
        for (int i = 0; i < theoryBookList.Count; i++)
        {
            if (theoryBookList[i].StartsWith(startString))
            {
                componentStartLine = i + 1;
            }
            if (theoryBookList[i].StartsWith(endString))
            {
                componentEndLine = i;
            }
        }

        for (int i = componentStartLine; i < componentEndLine; i++)
        {
            componentsLines.Add(theoryBookList[i]);
        }

    }
    public void ComponentBook()
    //opens component book while closing other books
    {
        TheoryBookComponent.SetActive(true);
        TheoryBookPipes.SetActive(false);
        TheoryBookDiagrams.SetActive(false);

        componentBtn.SetActive(false);
        pipeBtn.SetActive(true);
        diagramBtn.SetActive(true);

        Initialize();
    }
    public void PipeBook()
    //opens pipe book while closing other books
    {
        TheoryBookComponent.SetActive(false);
        TheoryBookPipes.SetActive(true);
        TheoryBookDiagrams.SetActive(false);

        componentBtn.SetActive(true);
        pipeBtn.SetActive(false);
        diagramBtn.SetActive(true);

        Initialize();
    }

    public void DiagramBook()
    //opens Diagram book while closing other books book
    {
        TheoryBookDiagrams.SetActive(true);
        TheoryBookPipes.SetActive(false);
        TheoryBookComponent.SetActive(false);

        componentBtn.SetActive(true);
        pipeBtn.SetActive(true);
        diagramBtn.SetActive(false);

        Initialize();
    }
}
