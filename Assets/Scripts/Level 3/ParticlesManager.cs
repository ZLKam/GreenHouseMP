using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3 {
    public class ParticlesManager : MonoBehaviour
    //handles spawning the moving particles through the pipes in the scene
    {
        public GameObject particle;

        private Coroutine spawnCoroutine;

        private bool spawnCoroutineRunning = false;

        public List<Vector2> targetPoints = new();

        public void SpawnParticle(Vector2 position)
        {
            spawnCoroutine = StartCoroutine(SpawnParticle(position, this));
        }

        //Stops spawning particles through the coroutine
        public void StopSpawnParticle()
        {
            if (!spawnCoroutineRunning)
                return;
            StopCoroutine(spawnCoroutine);
            spawnCoroutineRunning = false;
        }

        //instantiate a new particle every 0.2s continously, as the coroutine is triggered
        private IEnumerator SpawnParticle(Vector2 position, ParticlesManager manager)
        {
            while (true)
            {
                spawnCoroutineRunning = true;
                GameObject particle = Instantiate(this.particle, position, Quaternion.identity, transform);
                particle.GetComponent<Particle>().manager = manager;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
