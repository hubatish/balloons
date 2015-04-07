using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// place in center of movement range
/// </summary>
public class Patrol : MonoBehavclops
{
    public float speed = 2f;
    public Vector3 moveDirection = Vector3.right;

    public float timeToSwitch = 3f;

    protected void Start()
    {
        Delay(timeToSwitch / 2f, StartRealTimer);
    }

    protected void StartRealTimer()
    {
        Repeat(timeToSwitch, SwitchDirection);
    }

    protected void Update()
    {
        Cached<Transform>().Translate(moveDirection * speed*Time.deltaTime);
    }

    public void SwitchDirection()
    {
        moveDirection *= -1f;
    }
}

