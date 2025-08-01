using System;
using ImTipsyDude.IniD.Constants;
using ImTipsyDude.InstantECS;
using UnityEditor;
using UnityEngine;

namespace ImTipsyDude.System.IniD
{
    public class CmpObstacleGen : IECSComponent
    {
        public Vector3 ColliderSize;
        public float SpawnDistance;

        private void OnValidate()
        {
            var c = GetComponent<BoxCollider>();
            c.size = ColliderSize;
            c.isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var basepos = transform.position + (Vector3.forward * SpawnDistance);
            Gizmos.DrawLine(basepos + (Vector3.left * ColliderSize.x),
                basepos + (Vector3.right * ColliderSize.x));
        }
    }
}