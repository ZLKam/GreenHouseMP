using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public GameObject componentToSpawn;
    public Vector3 spawnPosition;

    public int spawnCount;
    GameObject[] components;

    public void SpawnObject()
    {
        components = GameObject.FindGameObjectsWithTag("Component");

        if (components.Length < spawnCount)
        {
            Instantiate(componentToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
