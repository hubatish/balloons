using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerBallMove : Singleton<PlayerBallMove>
{
    public Vector3 destVector = Vector3.right*100;

    public float speed = 2f;

    protected void Update()
    {
        float xDist = transform.position.x - destVector.x;
        Vector3 toMove = Vector3.right;
        transform.Translate(toMove * -xDist * speed * Time.deltaTime);
    }
}

