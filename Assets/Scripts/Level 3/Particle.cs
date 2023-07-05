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

        public Transform targetPos;

        private Image me;

        private int target = 0;

        // Start is called before the first frame update
        void Start()
        {
            me = GetComponent<Image>();
            flow = FindFirstObjectByType<CheckFlow>();
            me.rectTransform.position = flow.pipesTransform[0].position;
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = flow.pipesTransform[target];
            if (Vector2.Distance(me.rectTransform.position, targetPos.position) < 0.1f)
            {
                me.rectTransform.SetParent(targetPos.parent);
                if (target < flow.pipesTransform.Count - 1)
                {
                    if (me.rectTransform.parent != flow.pipesTransform[target + 1].parent)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            Vector2.MoveTowards(me.rectTransform.position, targetPos.position, 1f);
        }

        private IEnumerator Move()
        {
            yield return null;
        }
    }
}
