using System;
using ImTipsyDude.InstantECS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SysCheckPoint : IECSSystem
{
    private EnCheckPoint _checkPoint;

    public override void OnStart()
    {
        _checkPoint = GetEntity<EnCheckPoint>();
    }

    private void RandomInstantiate()
    {
        if (_checkPoint == null) return;

        for (int i = 0; i < 10; i++)
        {
            var offset = UnityEngine.Random.insideUnitSphere;
            if (_checkPoint.Prefab != null)
            {
                var param = new InstantiateParameters();
                param.scene = SceneManager.GetSceneByName("Level1");
                Instantiate(_checkPoint.Prefab,
                    transform.position + offset,
                    Quaternion.identity,
                    param);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RandomInstantiate();
        }
    }
}