using Level3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentWheel : MonoBehaviour
{
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

    public Text txtMode;

    // Start is called before the first frame update
    void Start()
    {
        playArea = GameObject.Find("PlayArea").transform;
        top = playArea.Find("Top");
        bottom = playArea.Find("Bottom");
        left = playArea.Find("Left");
        right = playArea.Find("Right");

        txtMode.text = "Select Component";
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
        selectComponent = !selectComponent;
        drawLine = !drawLine;

        if (selectComponent)
        {
            txtMode.text = "Select Component";
            FindObjectOfType<LineManagerController>().enabled = false;
        }
        else
        {
            txtMode.text = "Draw Pipe";
            FindObjectOfType<LineManagerController>().enabled = true;
        }
    }
}
