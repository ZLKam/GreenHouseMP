using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level3
{
    public class LineManager : MonoBehaviour
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
            checkFlow = FindObjectOfType<CheckFlow>();

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

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
            {
                CheckCanDrawLine();
            }
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
                {
                    // the mouse position is different from the initial position
                    if (mousePosition.x > initT.position.x)
                    {
                        // when mouse is at right side of the component
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
                {
                    lineManagerController.linesDrawn.Add(true);
                    lineManagerController.i++;
                    lineManagerController.CheckLineDrawn();
                    lineManagerController.componentClickedT = null;

                    checkFlow.AddPipe(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
                    checkFlow.AddPipe(lineRenderer.GetPosition(1), lineRenderer.GetPosition(2));
                    checkFlow.CheckAnswer();
                }
                else
                {
                    lineManagerController.componentClickedT = null;
                    Destroy(gameObject);
                }
            }
        }

        public Transform SetHitT
        {
            set { hitT = value; }
        }
    }
}