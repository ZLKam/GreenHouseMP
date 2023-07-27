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

    private List<string> componentsLines = new List<string>();
    private List<string> theoryBookList = new List<string>();

    private int componentStartLine = 0;
    private int componentEndLine = 0;
    private bool pipe;

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
    public GameObject TheoryBookTemplate;

    public GameObject previousBtn, nextBtn;

    public GameObject componentBtn, pipeBtn;
    public TextMeshProUGUI leftHeader, rightHeader;
    #endregion

    #region Page Components
    [Header("Pages")]
    [SerializeField]
    private List<Sprite> componentSprite = new List<Sprite>();
    [SerializeField]
    private List<Sprite> pipeSprite = new List<Sprite>();

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

        if (!pipe)
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
        else
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

        if (!pipe)
        {
            rightHeader.text = "COMPONENTS";
            ImageAssign(leftIamge, componentSprite, currentImage);
            ImageAssign(rightImage, componentSprite, currentImage + 1);
        }
        else 
        {
            rightHeader.text = "PIPE";
            ImageAssign(leftIamge, pipeSprite, currentImage);
            ImageAssign(rightImage, pipeSprite, currentImage + 1);
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

        for (int i = 0; i < theoryBookList.Count; i++)
        {
            if (!pipe)
            {
                if (theoryBookList[i].StartsWith("Components"))
                {
                    componentStartLine = i + 1;
                }
                if (theoryBookList[i].StartsWith("EndComponents"))
                {
                    componentEndLine = i;
                }
            }
            else if (pipe)
            {
                if (theoryBookList[i].StartsWith("Pipe"))
                {
                    componentStartLine = i + 1;
                }
                if (theoryBookList[i].StartsWith("EndPipes"))
                {
                    componentEndLine = i;
                }
            }
        }
        for (int i = componentStartLine; i < componentEndLine; i++)
        {
            componentsLines.Add(theoryBookList[i]);
        }

        leftRightArray = componentsLines[currentLine].Split(" & ");

        leftArray = leftRightArray[0].Split(" | ");
        rightArray = leftRightArray[1].Split(" | ");

        TextAssign(leftArray, leftTitle, leftDescription);
        TextAssign(rightArray, rightTitle, rightDescription);

        if (!pipe)
        {
            leftHeader.text = rightHeader.text = "COMPONENTS";
            ImageAssign(leftIamge, componentSprite, currentImage);
            ImageAssign(rightImage, componentSprite, currentImage + 1);
        }
        else
        {
            leftHeader.text = rightHeader.text = "PIPES";
            ImageAssign(leftIamge, pipeSprite, currentImage);
            ImageAssign(rightImage, pipeSprite, currentImage + 1);
        }
    }

    public void ComponentBook()
    //opens component book while closing pipe book
    {
        TheoryBookComponent.SetActive(true);
        TheoryBookPipes.SetActive(false);
        componentBtn.SetActive(false);
        pipeBtn.SetActive(true);

        pipe = TheoryBookPipes.activeSelf;
        Initialize();
    }
    public void PipeBook()
    //opens pipe book whiel closing component book
    {
        TheoryBookComponent.SetActive(false);
        TheoryBookPipes.SetActive(true);
        componentBtn.SetActive(true);
        pipeBtn.SetActive(false);

        pipe = TheoryBookPipes.activeSelf;
        Initialize();
    }
}
