using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysEnBolaObstacleGroup : IECSSystem
{
    private EnBolaObstacleGroup _entity;

    public override void OnStart()
    {
        _entity = GetEntity<EnBolaObstacleGroup>();
        var rbs = _entity.GetComponentsInChildren<Rigidbody>();
        _entity.Rigidbodies = rbs;
    }

    public void Explode()
    {
        foreach (var rb in _entity.Rigidbodies)
        {
            rb.AddExplosionForce(400,
                _entity.transform.position + _entity.ExplosionPosLocal,
                100
            );
        }
    }
}