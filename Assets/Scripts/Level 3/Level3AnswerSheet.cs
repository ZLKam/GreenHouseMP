using Level3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level3AnswerSheet : MonoBehaviour, IPointerDownHandler
{
    public LinePathFind line;

    private int numberOfCorrectConnections = 6;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (line.IsFindingPath())
            return;
        Debug.Log("Check answer");
        int correct = 0;
        FindObjectsOfType<DrawLine>().ToList().ForEach((x) =>
        {
            if (x.isCorrect())
            {
                correct++;
            }
        });
        if (correct == numberOfCorrectConnections)
        {
            Debug.Log("Correct!!!");
        }
        else
        {
            Debug.Log("You are so stupid!!! Wrong!!!!!!!!");
        }
    }
}
