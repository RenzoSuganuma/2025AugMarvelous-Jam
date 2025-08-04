using System;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class EnBolaObstacleGroup : IECSEntity
{
    public Vector3 ExplosionPosLocal;
    public Rigidbody[] Rigidbodies;
    public float CamStopTrackPlayerDirection;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var explosionPos = transform.position + ExplosionPosLocal;
        Gizmos.DrawSphere(explosionPos, 10);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(explosionPos, CamStopTrackPlayerDirection);
    }
}