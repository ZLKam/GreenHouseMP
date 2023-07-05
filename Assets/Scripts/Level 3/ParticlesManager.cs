using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3 {
    public class ParticlesManager : MonoBehaviour
    {
        public GameObject particle;

        public void StartToSpawnParticle(Transform pos = null)
        {
            StartCoroutine(SpawnParticle(pos));
        }

        public void StopSpawnParticle()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnParticle(Transform pos)
        {
            //while (true)
            //{
            //    GameObject particle = Instantiate(this.particle, pos.position, Quaternion.identity);
            //    particle.transform.parent = pos;
            //    yield return new WaitForSecondsRealtime(1f);
            //}
            GameObject particle = Instantiate(this.particle, pos.position, Quaternion.identity);
            particle.transform.parent = pos;
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
