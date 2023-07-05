using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Level3
{
    public class CheckFlow : MonoBehaviour
    {
        public GameObject particleManagerGO;
        private ParticlesManager particleManager;

        public List<Transform> pipesTransform = new();

        public Image particle;

        #region Test Transform
        public RectTransform start1;
        public RectTransform end1;
        public RectTransform start2;
        public RectTransform end2;
        public RectTransform start3;
        public RectTransform end3;
        public RectTransform start4;
        public RectTransform end4;
        #endregion

        private int correct = 0;
        private int answer = 8;

        private void Start()
        {
            particleManager = particleManagerGO.GetComponent<ParticlesManager>();
            //particle.rectTransform.position = pipesTransform[0].position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddPipe(start1, end1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddPipe(start2, end2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddPipe(start3, end3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                AddPipe(start4, end4);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckAnswer();
            }
        }

        internal void AddPipe(Transform start, Transform end)
        {
            if (pipesTransform.Contains(start) && pipesTransform.Contains(end))
            {
                return;
            }
            else if (pipesTransform.Contains(start) && !pipesTransform.Contains(end))
            {
                pipesTransform.Add(end);
            }
            else if (!pipesTransform.Contains(start) && pipesTransform.Contains(end))
            {
                pipesTransform.Add(start);
            }
            else
            {
                pipesTransform.Add(start);
                pipesTransform.Add(end);
            }
        }

        public void CheckAnswer()
        {
            particleManager.StartToSpawnParticle(pipesTransform[0]);
            //StartCoroutine(CheckParticleFlow());
        }

        //private IEnumerator CheckParticleFlow()
        //{
            //for (int i = 0; i < pipesTransform.Count; i++)
            //{
            //    if (i == 0)
            //    {
            // first point, instantiate particle

            //particle.rectTransform.position = pipesTransform[i].position;
            //particle.rectTransform.SetParent(pipesTransform[i].parent);
            //particleManager.StartToSpawnParticle(pipesTransform[i]);

            //}
            //else if (i % 2 == 0)
            //{
            // even point change particle position

            //particle.rectTransform.position = pipesTransform[i].position;
            //}
            //else if (i % 2 != 0)
            //{
            // odd point move towards the end of pipe
            //while (true)
            //{

            //if (Vector2.Distance(particle.rectTransform.position, pipesTransform[i].position) < 0.1f)
            //{
            //    particle.rectTransform.SetParent(pipesTransform[i].parent);
            //    if (i < pipesTransform.Count - 1)
            //    {
            //        if (particle.rectTransform.parent != pipesTransform[i + 1].parent)
            //        {
            //            Debug.Log("Con't connect, wrong pipe connection");
            //            yield break;
            //        }
            //    }
            //    break;
            //}
            //particle.rectTransform.position = Vector2.MoveTowards(particle.rectTransform.position, pipesTransform[i].position, 1f);
            //        yield return null;
            //    }
            //}
            //    correct ++;
            //yield return null;
            //}
            //if (correct == answer)
            //{
            //    Debug.Log("Correct");
            //}
            //else
            //{
            //    particle.rectTransform.position = pipesTransform[0].position;
            //    particle.rectTransform.SetParent(pipesTransform[0].parent);
            //    Debug.Log("Wrong");
            //}
        //}
    }
}