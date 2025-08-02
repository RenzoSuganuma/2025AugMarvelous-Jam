using System;
using ImTipsyDude.BolaBoom.Player;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using Unity.Cinemachine;
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

    private void Update()
    {
        GetEntity<EnBolaObstacleGroup>().World.CurrentScene
            .PullSystem(
                EnInstanceIdPool.Instance.Map[nameof(SysBolaBoomPlayerMove)],
                out SysBolaBoomPlayerMove result);
        if (Vector3.Distance(result.transform.position, _entity.transform.position) <=
            _entity.CamStopTrackPlayerDirection)
        {
            var c = Camera.main!.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera;
            if (c != null)
            {
                c.Follow = null;
            }
        }
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

    public void Explode(Vector3 position)
    {
        foreach (var rb in _entity.Rigidbodies)
        {
            rb.AddExplosionForce(400,
                position,
                100
            );
        }
    }
}