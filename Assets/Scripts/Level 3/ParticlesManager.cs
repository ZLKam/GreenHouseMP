using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3 {
    public class ParticlesManager : MonoBehaviour
    {
        public GameObject particle;

        private bool spawnCoroutineRunning = false;

        public void StartToSpawnParticle(Vector2 position)
        {
            StartCoroutine(SpawnParticle(position, this));
        }

        public void StopSpawnParticle()
        {
            if (!spawnCoroutineRunning)
                return;
            StopAllCoroutines();
            spawnCoroutineRunning = false;
        }

        private IEnumerator SpawnParticle(Vector2 position, ParticlesManager manager)
        {
            while (true)
            {
                spawnCoroutineRunning = true;
                GameObject particle = Instantiate(this.particle, position, Quaternion.identity);
                particle.GetComponent<Transform>().SetParent(transform);
                particle.GetComponent<Particle>().manager = manager;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
