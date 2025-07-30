using System;
using UnityEngine;

namespace ImTipsyDude.InstantECS
{
    /// <summary>
    /// NOTE:
    /// システムにはデータに基づいた機能を集める（実装する）
    /// </summary>
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    [RequireComponent(typeof(IECSEntity))]
    public abstract class IECSSystem : MonoBehaviour
    {
        private IECSEntity _currentEntity;

        public int ID => GetInstanceID();

        public abstract void OnStart();

        public IECSEntity Entity => _currentEntity;

        public T GetEntity<T>() where T : IECSEntity => _currentEntity as T;

        private void Start()
        {
            _currentEntity = gameObject.GetComponent<IECSEntity>();
            IECSWorld.GetScene().BindSystem(this);
            OnStart();
        }
    }
}