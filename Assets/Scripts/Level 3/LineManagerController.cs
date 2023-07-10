using QPathFinder;
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
        public GameObject emptyGO;
        public GameObject emptyLinePrefab;

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
                //checks if you have hit the UI to prevent lines being drawn there
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.layer == 5)
                {
                    Debug.Log("hit UI");
                    return;
                }
                if (componentClicked)
                {
                    Debug.Log("hit component");
                    
                    //checks if there is a line already being drawn
                    //if there is a child under lineboss, and the line is defined to be still being drawn, will return
                    if (!(transform.childCount == 0) && transform.GetChild(transform.childCount - 1).GetComponent<LineManager>().isDrawing)
                    {
                        Debug.Log("line is drawing");
                        return;
                    }
                    
                    Debug.Log("instantiating line");
                    //draws the line and sets it as a child to the object this script is attached to(lineboss)
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
        //resets the component clicked?
        {
            componentClicked = false;
        }

        public void CheckLineDrawn()
        {
            // adds linesToDraw to a component when it is instantiated
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