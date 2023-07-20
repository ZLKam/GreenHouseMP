using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace Test
{
    public class TestDialogue : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Image personA;
        private Image personB;

        [SerializeField]
        private TextAsset dialogueFile;
        private TextMeshProUGUI text;

        private List<string> dialogueLines;
        private int currentLine = 0;

        // Start is called before the first frame update
        void Start()
        {
            // Load the text file
            dialogueFile = Resources.Load<TextAsset>("TestDialogue");
            string[] lines = dialogueFile.text.Split('\n');
            dialogueLines = lines.ToList();
            text = GetComponentInChildren<TextMeshProUGUI>();
            personA = transform.Find("PersonA").GetComponent<Image>();
            personB = transform.Find("PersonB").GetComponent<Image>();
            personA.gameObject.SetActive(false);
            personB.gameObject.SetActive(false);            text.text = CheckCurrentTextBehaviour(currentLine); ;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            NextLine();
        }

        private void NextLine()
        {
            if (currentLine < dialogueLines.Count - 1)
            {
                currentLine++;
                text.text = CheckCurrentTextBehaviour(currentLine);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private string CheckCurrentTextBehaviour(int currentLine)
        {
            string textToCheck = string.Empty;
            string textToShow = string.Empty;
            if (dialogueLines[currentLine].StartsWith("[A]"))
            {
                personA.gameObject.SetActive(true);
                personB.gameObject.SetActive(false);
                textToCheck = dialogueLines[currentLine].Replace("[A]", "");
                textToShow = PersonEmotion(textToCheck, personA);
            }
            else if (dialogueLines[currentLine].StartsWith("[B]"))
            {
                personB.gameObject.SetActive(true);
                personA.gameObject.SetActive(false);
                textToCheck = dialogueLines[currentLine].Replace("[B]", "");
                textToShow = PersonEmotion(textToCheck, personB);
            }
            else
            {
                textToShow = dialogueLines[currentLine];
            }
            return textToShow;
        }

        private string PersonEmotion(string text, Image person)
        {
            string textToReturn = string.Empty;
            if (text.Contains("[a0]"))
            {
                textToReturn = text.Replace("[a0]", "");
                StartCoroutine(ChangeColorCo(person, Color.white, person.color));
            }
            else
            {
                textToReturn = text;
            }
            return textToReturn;
        }

        private IEnumerator ChangeColorCo(Image person, Color colorToChange, Color colorOriginal)
        {
            yield return new WaitForSeconds(0.5f);
            person.color = colorToChange;
            yield return new WaitForSeconds(1f);
            person.color = colorOriginal;
        }
    }
}