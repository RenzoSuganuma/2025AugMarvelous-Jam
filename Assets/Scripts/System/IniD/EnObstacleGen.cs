using ImTipsyDude.InstantECS;
using UnityEngine;

namespace ImTipsyDude.System.IniD
{
    public class EnObstacleGen : IECSEntity
    {
        public GameObject[] Obstacles;
        public LayerMask PLayerMask;
    }
}