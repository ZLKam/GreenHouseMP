using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

namespace Level3
{
    public class DrawLine : MonoBehaviour
    {
        public LineRenderer lr;
        ParticlesManager particlesManager;
        internal RectGrid rectGrid;

        internal bool finishedAddingPoints = false;

        public List<Vector2> points = new();

        private List<Vector3> vector3Points = new();

        [SerializeField]
        private float lineThickness = 0.3f;
        [SerializeField]
        internal Color lineColor = new Color(255, 255, 255);
        [SerializeField]
        private Material lineMat;

        [SerializeField]
        internal Transform lineFrom;
        [SerializeField]
        internal Transform lineTo;

        // Start is called before the first frame update
        void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.startWidth = lineThickness;
            lr.endWidth = lineThickness;
            lr.startColor = lineColor;
            lr.endColor = lineColor;
            lr.material = Instantiate(lineMat);
            lr.material.color = lineColor;

            particlesManager = GetComponent<ParticlesManager>();

            StartCoroutine(Draw());
        }

        private IEnumerator Draw()
        {
            while (finishedAddingPoints)
            {
                points.ForEach(x => vector3Points.Add(new Vector3(x.x, x.y, 0f)));
                lr.positionCount = vector3Points.Count;
                lr.SetPositions(vector3Points.ToArray());
                particlesManager.targetPoints = points;
                StartCoroutine(particlesManager.SpawnParticle(points[0], particlesManager));
                yield break;
            }
        }

        public void ResetCellToWalkable()
        {
            //points.ForEach(point => rectGrid.transform.Find("cell_" + point.x + "_" + point.y).GetComponent<RectGridCell>());

            int index = 0;
            points.ForEach((x) =>
            {
                Vector2 point = points[index];
                Transform cell = rectGrid.transform.Find("cell_" + point.x + "_" + point.y);
                if (cell)
                {
                    cell.GetComponent<RectGridCell>()?.SetWalkable();
                }
                index++;
            });
        }

        public bool isCorrect()
        {
            ComponentEvent component = lineFrom.GetComponentInParent<ComponentEvent>();
            // check if the line is connected to the correct target
            if (component.correctTarget.GetComponent<ComponentEvent>().specialID == lineTo.parent.GetComponent<ComponentEvent>().specialID ||
                (component.correctTarget2 ? component.correctTarget2.GetComponent<ComponentEvent>().specialID == lineTo.parent.GetComponent<ComponentEvent>().specialID : false))
            {
                // check if the line is the correct type
                if (component.GetComponent<ComponentEvent>().correctColor == lineColor ||
                    (component.correctTarget2 ? component.GetComponent<ComponentEvent>().correctColor2 == lineColor : false))
                {
                    return true;
                }
            }
            return false;
        }
    }
}