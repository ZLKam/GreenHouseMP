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

        public Transform targetPos;

        private Image me;

        private int target = 0;
        private float speed = 100f;

        // Start is called before the first frame update
        void Start()
        {
            me = GetComponent<Image>();
            flow = FindFirstObjectByType<CheckFlow>();
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            if (flow.pipesTransform.Count == 1)
            {
                Debug.Log("no pipe");
                yield break;
            }
            for (int i = 1; i < flow.pipesTransform.Count; i++)
            {
                target = i;
                if (i % 2 != 0)
                {
                    targetPos = flow.pipesTransform[target];
                    while (true)
                    {
                        if (Vector2.Distance(me.rectTransform.position, targetPos.position) < 0.1f)
                        {
                            me.rectTransform.SetParent(targetPos.parent);
                            if (target < flow.pipesTransform.Count - 1)
                            {
                                if (me.rectTransform.parent != flow.pipesTransform[target + 1].parent)
                                {
                                    Debug.Log("wrong parent");
                                    yield break;
                                }
                            }
                            else
                            {
                                manager.StopSpawnParticle();
                                Destroy(gameObject);
                            }
                            break;
                        }
                        me.rectTransform.position = Vector2.MoveTowards(me.rectTransform.position, targetPos.position, speed * Time.deltaTime);
                        yield return null;
                    }
                }
                else if (i % 2 == 0)
                {
                    me.rectTransform.position = flow.pipesTransform[i].position;
                }
            }
            yield return null;
        }
    }
}
