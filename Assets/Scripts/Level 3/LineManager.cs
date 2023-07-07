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

        public Vector3 startPos;

        private Vector3 mousePosition = new();

        [SerializeField]
        internal bool isDrawing = false;

        [SerializeField]
        private float timePressed = 0f;

        [SerializeField]
        private Transform hitT;

        private void Start()
        {
            lineManagerController = transform.parent.GetComponent<LineManagerController>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            startPos = mousePosition;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, startPos);
            lineRenderer.SetPosition(2, startPos);
            lineRenderer.SetPosition(3, startPos);
            isDrawing = true;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (Input.GetMouseButtonDown(0))
            {
                if (isDrawing)
                {
                    isDrawing = false;
                    if (lineManagerController.componentClicked && lineManagerController.componentClickedT != hitT)
                    {
                        lineManagerController.linesDrawn.Add(true);
                        lineManagerController.i++;
                        lineManagerController.CheckLineDrawn();
                        lineManagerController.componentClickedT = null;
                    }
                    else
                    {
                        lineManagerController.componentClickedT = null;
                        Destroy(gameObject);
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                timePressed += Time.deltaTime;
            }
            if (timePressed >= 1f)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (isDrawing)
                    {
                        timePressed = 0f;
                        isDrawing = false;
                        if (lineManagerController.componentClicked && lineManagerController.componentClickedT != hitT)
                        {
                            lineManagerController.linesDrawn.Add(true);
                            lineManagerController.i++;
                            lineManagerController.CheckLineDrawn();
                            lineManagerController.componentClickedT = null;
                        }
                        else
                        {
                            lineManagerController.componentClickedT = null;
                            Destroy(gameObject);
                        }
                    }
                }
            }

            if (lineRenderer.enabled && isDrawing)
            {
                Vector3 startPos = lineRenderer.GetPosition(0);
                Vector3 half = (mousePosition + startPos) / 2;
                lineRenderer.SetPosition(1, new Vector3(startPos.x, half.y, half.z));
                lineRenderer.SetPosition(2, new Vector3(mousePosition.x, half.y, half.z));
                lineRenderer.SetPosition(3, mousePosition);
            }
        }

        public Transform SetHitT
        {
            set { hitT = value; }
        }
    }
}