using Level3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level3AnswerSheet : MonoBehaviour, IPointerClickHandler
{
    public LinePathFind line;
    public Fade fade;

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

    List<KeyValuePair<DrawLine, Material>> wrongLinesPreviousMaterials = new();

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
        correctLines.ForEach(x => x.lr.material = correctMat);
        wrongLines.ForEach((wrongLine) =>
        {
            KeyValuePair<DrawLine, Material> previousMaterial = new KeyValuePair<DrawLine, Material>(wrongLine, wrongLine.lr.material);
            wrongLinesPreviousMaterials.Add(previousMaterial);
            wrongLine.lr.material = wrongMat;
        });
        if (correct == numberOfCorrectConnections)
        {
            correctPanel.SetActive(true);
            if (!PlayerPrefs.HasKey(Strings.ChapterTwoLevelThreeCompleted))
            {
                if (PlayerPrefs.HasKey(Strings.ChapterTwoProgressions))
                {
                    int progress = PlayerPrefs.GetInt(Strings.ChapterTwoProgressions);
                    progress++;
                    PlayerPrefs.SetInt(Strings.ChapterTwoProgressions, progress);
                }
                else
                {
                    PlayerPrefs.SetInt(Strings.ChapterTwoProgressions, 1);
                }
                PlayerPrefs.SetInt(Strings.ChapterTwoLevelThreeCompleted, 1);
            }
            fade.ShowChapterTwoBadge();
            Debug.Log("Correct!!!");
        }
        else
        {
            wrongPanel.SetActive(true);
            StartCoroutine(ResetLineColor(wrongPanel.GetComponent<PopUp>().timer, wrongLines));
            Debug.Log("You are so stupid!!! Wrong!!!!!!!!");
        }
        correctLines.Clear();
        wrongLines.Clear();
    }

    private IEnumerator ResetLineColor(float time, List<DrawLine> wrongLines)
    {
        yield return new WaitForSeconds(time);
        Debug.Log(time);
        wrongLinesPreviousMaterials.ForEach(x => x.Key.lr.material = x.Value);
        wrongLinesPreviousMaterials.Clear();
    }
}
