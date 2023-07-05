using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3 {
    public class ParticlesManager : MonoBehaviour
    {
        public GameObject particle;

        private bool spawnCoroutineRunning = false;

        public void StartToSpawnParticle(Transform pos = null)
        {
            StartCoroutine(SpawnParticle(pos, this));
        }

        public void StopSpawnParticle()
        {
            if (!spawnCoroutineRunning)
                return;
            StopAllCoroutines();
            spawnCoroutineRunning = false;
        }

        private IEnumerator SpawnParticle(Transform pos, ParticlesManager manager)
        {
            while (true)
            {
                spawnCoroutineRunning = true;
                GameObject particle = Instantiate(this.particle, pos.position, Quaternion.identity);
                particle.GetComponent<RectTransform>().SetParent(pos);
                particle.GetComponent<Particle>().manager = manager;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
