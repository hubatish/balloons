using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    protected float explosionForce = 19.0f;
    protected float explosionRadius = 15.0f;

    protected void OnMouseDown()
    {
        Debug.Log("balloon pressed");
        Pop();
    }

    public void Pop()
    {
        AddExplosionForce(explosionForce,transform.position,explosionRadius);
        GameObject.Destroy(gameObject);
    }

    public static void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        Rigidbody[] allRigidbodies = Rigidbody.FindObjectsOfType<Rigidbody>();
        foreach(var body in allRigidbodies)
        {
            body.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }
}

