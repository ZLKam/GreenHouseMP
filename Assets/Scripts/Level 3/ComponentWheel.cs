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

    //public Text txtMode;
    public Sprite drawLineSprite;
    public Sprite selectComponentSprite;

    public ChangeMode btnChangeMode;
    internal bool SelectComponent { get { return btnChangeMode.SelectComponent; } }
    internal bool DrawLine { get { return btnChangeMode.DrawLine; } }

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
