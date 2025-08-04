using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using R3;
using UnityEngine.Serialization;

namespace ImTipsyDude.IniD.Player
{
    public class CmpIniDPlayer : IECSComponent
    {
        public float CurrentMaxSpeed;
        public float MaxSpeed;

        public override void OnStart()
        {
            EnInstanceIdPool.Instance.Map.Add( nameof( CmpIniDPlayer ) , ID );
        }
    }
}