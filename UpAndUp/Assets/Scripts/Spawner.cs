using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Spawner : MonoBehavclops
{
    public GameObject toSpawn;
    public float spawnTime = 0.9f;
    public float variedTime = 0.1f;

    protected void Start()
    {
        Repeat(spawnTime,variedTime,SpawnThing);
    }

    public void SpawnThing()
    {
        GameObject.Instantiate(toSpawn, Cached<Transform>().position, Quaternion.identity);
    }
}

