using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level3
{
    public class LineManager : MonoBehaviour
    {
        LineRenderer lineRenderer;

        private Vector3 mousePosition = new();
        [SerializeField]
        internal bool isDrawing = false;

        [SerializeField]
        private float timePressed = 0f;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, mousePosition);
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
                    transform.parent.GetComponent<LineManagerController>().linesDrawn.Add(true);
                    transform.parent.GetComponent<LineManagerController>().i++;
                    transform.parent.GetComponent<LineManagerController>().CheckLineDrawn();
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
                        transform.parent.GetComponent<LineManagerController>().linesDrawn.Add(true);
                        transform.parent.GetComponent<LineManagerController>().i++;
                        transform.parent.GetComponent<LineManagerController>().CheckLineDrawn();
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
    }
}