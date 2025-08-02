using ImTipsyDude.InstantECS;
using UnityEngine;
using UnityEngine.Serialization;

namespace ImTipsyDude.System.IniD
{
    public class EnObstacleGen : IECSEntity
    {
        public GameObject[] Obstacles;
        public LayerMask PlayerMask;
    }
}