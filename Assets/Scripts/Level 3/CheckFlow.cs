using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Level3
{
    public class CheckFlow : MonoBehaviour
    {
        private ParticlesManager particleManager;

        public List<Vector2> pipesTransform = new();

        private void Start()
        {
            particleManager = GetComponent<ParticlesManager>();
        }

        internal void AddPipe(Vector2 start, Vector2 end)
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
        }
    }
}