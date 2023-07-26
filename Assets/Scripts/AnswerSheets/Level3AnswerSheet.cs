using Level3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level3AnswerSheet : MonoBehaviour, IPointerClickHandler
{
    public LinePathFind line;

    private int numberOfCorrectConnections = 6;

    List<DrawLine> correctLines = new();
    List<DrawLine> wrongLines = new();

    [SerializeField]
    private Material correctMat;
    [SerializeField]
    private Material wrongMat;

    [SerializeField]
    private GameObject correctPanel;
    [SerializeField]
    private GameObject wrongPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (line.IsFindingPath())
            return;

        Debug.Log("Check answer");
        int correct = 0;
        FindObjectsOfType<DrawLine>().ToList().ForEach((x) =>
        {
            if (x.isCorrect())
            {
                correctLines.Add(x);
                correct++;
            }
            else
            {
                wrongLines.Add(x);
            }
        });
        if (correct == numberOfCorrectConnections)
        {
            correctLines.ForEach(x => x.lr.material = correctMat);
            correctPanel.SetActive(true);
            Debug.Log("Correct!!!");
        }
        else
        {
            wrongLines.ForEach(x => x.lr.material = wrongMat);
            wrongPanel.SetActive(true);
            Debug.Log("You are so stupid!!! Wrong!!!!!!!!");
        }
    }

}
