using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level3
{
    public class LineManager : MonoBehaviour
    //handles the state of the line
    {
        LineRenderer lineRenderer;
        LineManagerController lineManagerController;
        ComponentWheel componentWheel;
        CheckFlow checkFlow;

        public Vector3 startPos;

        private Vector3 mousePosition = new();

        [SerializeField]
        internal bool isDrawing = false;

        [SerializeField]
        private float timePressed = 0f;

        [SerializeField]
        private Transform initT;
        [SerializeField]
        private Transform hitT;
        private Vector2 centerPoint;

        private void Start()
        {
            componentWheel = FindObjectOfType<ComponentWheel>();
            centerPoint = componentWheel.centerPointOfPlayArea;
            lineManagerController = transform.parent.GetComponent<LineManagerController>();
            initT = lineManagerController.componentClickedT;
            //initial transform is set as the component that has been interacted with(iniT)
            checkFlow = FindObjectOfType<CheckFlow>();

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            //line variables

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            startPos = mousePosition;

            lineRenderer.enabled = true;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, startPos);
            isDrawing = true;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (Input.GetMouseButtonDown(0))
            //if left click, run the function to draw lines
            {
                CheckCanDrawLine();
            }

            //handles the cooldown of drawing lines, prevent spam and multiple lines drawn at once
            if (Input.GetMouseButton(0))
            {
                timePressed += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (timePressed >= 1f)
                {
                    CheckCanDrawLine();
                }
                timePressed = 0f;
            }

            if (lineRenderer.enabled && isDrawing)
            {
                //Vector3 startPos = lineRenderer.GetPosition(0);
                //Vector3 half = (mousePosition + startPos) / 2;
                //lineRenderer.SetPosition(1, new Vector3(startPos.x, half.y, half.z));
                //lineRenderer.SetPosition(2, new Vector3(mousePosition.x, half.y, half.z));
                //lineRenderer.SetPosition(3, mousePosition);

                if (mousePosition != initT.position)
                //if the current mouse position is not already on the component the line is starting from
                {
                    if (mousePosition.x > initT.position.x)
                    // when the mouse position has moved beyond the initial component x axis position in the positive direction
                    {
                        //sets the line position to the mouse position then again?
                        lineRenderer.positionCount = 3;
                        lineRenderer.SetPosition(1, new Vector2(mousePosition.x, startPos.y));
                        lineRenderer.SetPosition(2, mousePosition);
                    }
                }
            }
        }

        private void CheckCanDrawLine()
        {
            if (isDrawing)
            {
                isDrawing = false;
                if (lineManagerController.componentClicked && initT != hitT)
                //if there is a component that has been interacted with
                //and the initial component(iniT) does not match the new component(hitT) that has been interacted with
                {
                    //adds the new line drawn into a list of lines drawn
                    lineManagerController.linesDrawn.Add(true);
                    lineManagerController.i++;
                    lineManagerController.CheckLineDrawn();
                    lineManagerController.componentClickedT = null;

                    //handles generating the flow(particles) and the positions of the end and beginning points for the particles to flow
                    checkFlow.AddPipe(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
                    checkFlow.AddPipe(lineRenderer.GetPosition(1), lineRenderer.GetPosition(2));
                    checkFlow.CheckAnswer();
                }
                else
                {
                    //sets the component clicked to null and destroys the line
                    lineManagerController.componentClickedT = null;
                    Destroy(gameObject);
                }
            }
        }

        //for reference to set the hitT variable in LineManagerController script
        public Transform SetHitT
        {
            set { hitT = value; }
        }
    }
}