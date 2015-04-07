using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BalloonFloat : MonoBehavclops
{
    public float speed = 2f;

    protected void Start()
    {

    }

    protected void Update()
    {
        Cached<Transform>().Translate(Vector3.up * speed * Time.deltaTime);
    }
}

