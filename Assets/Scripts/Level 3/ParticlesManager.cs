using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3 {
    public class ParticlesManager : MonoBehaviour
    //handles spawning the moving particles through the pipes in the scene
    {
        public GameObject particle;
        public GameObject arrow;

        private Coroutine particleCoroutine;
        private Coroutine arrowCoroutine;

        private bool spawnCoroutineRunning = false;
        private bool arrowCoroutineRunning = false;

        public List<Vector2> targetPoints = new();

        public void SpawnParticle(Vector2 position)
        {
            particleCoroutine = StartCoroutine(SpawnParticle(position, this));
        }

        public void SpawnArrow(Vector2 position)
        {
            arrowCoroutine = StartCoroutine(SpawnArrow(position, this));
        }

        //Stops spawning particles through the coroutine
        public void StopSpawnParticle()
        {
            if (!spawnCoroutineRunning)
                return;
            StopCoroutine(particleCoroutine);
            spawnCoroutineRunning = false;
        }

        public void StopSpawnArrow()
        {
            if (!arrowCoroutineRunning)
                return;
            StopCoroutine(arrowCoroutine);
            arrowCoroutineRunning = false;
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

        private IEnumerator SpawnArrow(Vector2 position, ParticlesManager manager)
        {
            while (true)
            {
                arrowCoroutineRunning = true;
                GameObject particle = Instantiate(arrow, position, Quaternion.identity, transform);
                particle.GetComponent<Particle>().manager = manager;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
