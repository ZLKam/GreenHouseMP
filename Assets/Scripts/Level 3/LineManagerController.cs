using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class LineManagerController : MonoBehaviour
    {
        public GameObject lineManager;
        public ComponentWheel wheel;

        public int linesToDraw = 0;
        [SerializeField]
        internal int i = 0;
        public List<bool> linesDrawn = new();

        public bool componentClicked = false;

        public Transform componentClickedT = null;

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
                if (componentClicked)
                {
                    Debug.Log("hit component");
                    
                    if (!(transform.childCount == 0) && transform.GetChild(transform.childCount - 1).GetComponent<LineManager>().isDrawing)
                    {
                        Debug.Log("line is drawing");
                        return;
                    }
                    Instantiate(lineManager, transform.position, Quaternion.identity, transform);
                    return;
                }

                //if (i == linesToDraw)
                //{
                //    Debug.Log("Finished drawing line. Check answer");
                //}
            }
        }

        private void LateUpdate()
        {
            componentClicked = false;
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