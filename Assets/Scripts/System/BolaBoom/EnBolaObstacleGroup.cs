using System;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class EnBolaObstacleGroup : IECSEntity
{
    public Vector3 ExplosionPosLocal;
    public Rigidbody[] Rigidbodies;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(transform.position + ExplosionPosLocal, 10);
    }
}