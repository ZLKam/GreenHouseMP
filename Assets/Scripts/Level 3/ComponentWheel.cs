using Level3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComponentWheel : MonoBehaviour
{
    public RectGrid rectGrid;
    public LinePathFind linePathFind;

    [SerializeField]
    internal Transform playArea;
    private Transform top;
    private Transform bottom;
    private Transform left;
    private Transform right;

    [SerializeField]
    internal bool selectComponent = true;
    [SerializeField]
    internal bool drawLine = false;

    //public Text txtMode;
    public Image btnChangeMode;
    public Sprite drawLineSprite;
    public Sprite selectComponentSprite;

    public Vector2 centerPointOfPlayArea = new();

    private List<bool> correctList = new();
    private int numberOfCorrectConnections = 5;

    // Start is called before the first frame update
    void Start()
    {
        playArea = GameObject.Find("PlayArea").transform;
        top = playArea.Find("Top");
        bottom = playArea.Find("Bottom");
        left = playArea.Find("Left");
        right = playArea.Find("Right");

        centerPointOfPlayArea = new Vector2((top.position.y + bottom.position.y) / 2, (left.position.x + right.position.x) / 2);

        //txtMode.text = "Select Component";
        btnChangeMode.sprite = selectComponentSprite;
    }

    internal bool IsWithinX(Vector3 check)
    {
        if (check.x < right.position.x && check.x > left.position.x)
        {
            return true;
        }
        return false;
    }

    internal bool IsWithinY(Vector3 check)
    {
        if (check.y < top.position.y && check.y > bottom.position.y)
        {
            return true;
        }
        return false;
    }

    public void ChangeMode()
    {
        if (linePathFind.IsFindingPath())
            return;
        selectComponent = !selectComponent;
        drawLine = !drawLine;

        if (selectComponent)
        {
            //txtMode.text = "Select Component";
            btnChangeMode.sprite = selectComponentSprite;
            //FindObjectOfType<LineManagerController>().enabled = false;
            //var lineParents = GameObject.FindGameObjectsWithTag("LineParent").ToList();
            //var lines = GameObject.FindGameObjectsWithTag("Line").ToList();
            //GameObject lineBoss = FindObjectOfType<LineManagerController>().gameObject;
            //List<GameObject> allLines = new();
            //allLines.AddRange(lineParents);
            //allLines.AddRange(lines);
            //allLines.Remove(lineBoss);
            //allLines.ForEach(line => Destroy(line));

            //foreach (Transform child in playArea)
            //{
            //    if (child.CompareTag("Component"))
            //    {
            //        child.GetComponent<ComponentEvent>().ResetAllowDraw();
            //        child.GetComponent<ComponentEvent>().CorrectConnection = false;
            //    }
            //}
            GameObject lineBoss = FindObjectOfType<LinePathFind>().gameObject;
            lineBoss.GetComponent<LinePathFind>().enabled = false;
            List<GameObject> allLines = new();
            for (int i = 0; i < lineBoss.transform.childCount; i++)
            {
                allLines.Add(lineBoss.transform.GetChild(i).gameObject);
            }
            FindObjectsOfType<LineLimit>().ToList().ForEach(x => x.AllowDrawLine = true);
            allLines.ForEach(line => Destroy(line));

            FindObjectsOfType<DrawLine>().ToList().ForEach(x => x.ResetCellToWalkable());
        }
        else
        {
            //txtMode.text = "Draw Line";
            btnChangeMode.sprite = drawLineSprite;
            //FindObjectOfType<LineManagerController>().enabled = true;
            FindObjectOfType<LinePathFind>().enabled = true;
        }
    }

    public void ResetScene()
    {
        Instructions.Read = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void CheckAnswer()
    //{
        //foreach (Transform child in playArea)
        //{
        //    if (child.CompareTag("Component") || child.CompareTag("Component/Chiller"))
        //    {
        //        correctList.Add(child.GetComponent<ComponentEvent>().CorrectConnection);
        //    }
        //}
        //Debug.Log(correctList.Count);
        //if (correctList.Count >= numberOfCorrectConnections)
        //{
        //    foreach (bool correct in correctList)
        //    {
        //        if (!correct)
        //        {
        //            Debug.Log("Incorrect");
        //            correctList.Clear();
        //            return;
        //        }
        //    }
        //    Debug.Log("Correct");
        //}
        //else
        //{
        //    Debug.Log("Incorrect");
        //}
        //correctList.Clear();
    //}
}
