using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class DrawLine : MonoBehaviour
    {
        LineRenderer lr;
        ParticlesManager particlesManager;

        internal bool finishedAddingPoints = false;

        public List<Vector2> points = new();

        private List<Vector3> vector3Points = new();

        [SerializeField]
        private float lineThickness = 0.1f;
        [SerializeField]
        private Color lineColor = new Color(0, 0, 0);
        [SerializeField]
        private Material lineMat;

        // Start is called before the first frame update
        void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.startWidth = lineThickness;
            lr.endWidth = lineThickness;
            lr.startColor = lineColor;
            lr.endColor = lineColor;
            lr.material = lineMat;

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
    }
}