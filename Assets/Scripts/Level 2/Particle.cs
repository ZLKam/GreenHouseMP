using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particle : MonoBehaviour
{
    public List<Transform> flowPoints;
    public float moveSpeed;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        flowPoints = GetComponentInParent<ParticleFlow>().flowPoints;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = flowPoints[index].transform.position;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.position = newPosition;

        float distance = Vector3.Distance(transform.position, destination);

        if (distance <= 0.05f)
        {
            if(index < flowPoints.Count - 1)
            {
                index++;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
