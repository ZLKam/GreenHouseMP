using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class LineManagerController : MonoBehaviour
    {
        public int linesToDraw = 0;
        [SerializeField]
        internal int i = 0;
        public List<bool> linesDrawn = new();

        private void Start()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.layer == 5)
                {
                    Debug.Log("hit UI");
                    return;
                }
                if (i >= transform.childCount)
                {
                    return;
                }
                transform.GetChild(i).gameObject.SetActive(true);

                if (i == linesToDraw)
                {
                    Debug.Log("Finished drawing line. Check answer");
                }
            }
        }

        public void CheckLineDrawn()
        {
            // linesToDraw add when a component added
            // check linesDrawn after every line drawn, add a bool every time draw a line
            foreach (bool lineDrawn in linesDrawn)
            {
                if (!lineDrawn)
                {
                    return;
                }
            }
            if (linesDrawn.Count == linesToDraw)
            {
                Debug.Log("all line connected");
            }
        }
    }
}