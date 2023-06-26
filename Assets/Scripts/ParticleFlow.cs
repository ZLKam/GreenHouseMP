using System.Collections.Generic;
using UnityEngine;

public class ParticleFlow : MonoBehaviour
{
    Transform[] children;
    public float spawnInterval = .25f;
    public GameObject particlePrefab;

    public List<Transform> flowPoints;

    float startTime;
    float spawnTime;
    bool spawnParticle;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        spawnTime = spawnInterval;

        particlePrefab = FindObjectOfType<Connection>().particle;
        var points = new List<Transform>();
        children = gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == "point")
            {
                points.Add(children[i]);
            }
            flowPoints = points;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // The task is done waiting if the time waitDuration has elapsed since the task was started.
        if (startTime + spawnTime < Time.time)
        {
            spawnParticle = true;
        }

        if (spawnParticle)
        {
            var particle = Instantiate(particlePrefab, flowPoints[0].transform.position, particlePrefab.transform.rotation);
            particle.transform.parent = gameObject.transform;

            startTime = Time.time;
            spawnParticle = false;
        }
    }
}
