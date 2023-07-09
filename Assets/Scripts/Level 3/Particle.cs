using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

namespace Level3
{
    public class Particle : MonoBehaviour
    {
        CheckFlow flow;
        
        internal ParticlesManager manager;

        public Vector2 targetPos;

        private int target = 0;
        private float speed = 10f;

        // Start is called before the first frame update
        void Start()
        {
            flow = FindFirstObjectByType<CheckFlow>();
            StartCoroutine(Move());
        }

        //Coroutine to move the particles across the pipes
        private IEnumerator Move()
        {
            if (flow.pipesTransform.Count == 1)
            //if there is no end point to the pipes
            {
                Debug.Log("no pipe");
                yield break;
            }
            for (int i = 1; i < flow.pipesTransform.Count; i++)
            {
                target = i;
                targetPos = flow.pipesTransform[target];
                while (true)
                {
                    if (Vector2.Distance(transform.position, targetPos) < 0.1f)
                    {
                        if (target >= flow.pipesTransform.Count - 1)
                        {
                            //manager.StopSpawnParticle();
                            Destroy(gameObject);
                        }
                        break;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                    yield return null;
                }
            }
            yield return null;
        }
    }
}
