using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsObjective : MonoBehaviour
{
    // Just write the short name as stated in the LevelsObjective.txt file in the hierachy
    [SerializeField]
    private string chapter = "";
    [SerializeField]
    private string currentLevel = "";

    public Image profileImage;
    public TextMeshProUGUI objectiveText;
    public GameObject instructionPanel;

    private TextAsset levelsObjective;
    private List<string> levelsObjectiveLines;

    // Start is called before the first frame update
    void Start()
    {
        levelsObjective = Resources.Load<TextAsset>("TextFiles/LevelsObjective");

        if (PlayerPrefs.HasKey(Strings.ProfileImage))
        {
            Texture2D texture2D = new(1, 1);
            texture2D.LoadImage(Convert.FromBase64String(PlayerPrefs.GetString(Strings.ProfileImage)));
            profileImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        }
        levelsObjectiveLines = levelsObjective.text.Split('\n').ToList();
        ShowObjectiveText(chapter, currentLevel);
    }

    private void ShowObjectiveText(string chapter, string level)
    {
        levelsObjectiveLines.ForEach((line) =>
        {
            if (line.StartsWith("//"))
            {
                return;
            }
            if (line.StartsWith($"[{chapter}][{level}]"))
            {
                objectiveText.text = line[(chapter.Length + level.Length + 4)..];
            }
        });
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        instructionPanel.SetActive(true);
    }
}
