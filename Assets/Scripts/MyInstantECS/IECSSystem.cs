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
        public int ID => GetInstanceID();

        public abstract void OnStart();

        public T GetEntity<T>() where T : IECSEntity => gameObject.GetComponent<T>();

        private void Start()
        {
            IECSWorld.GetScene().BindSystem(this);
            OnStart();
        }
    }
}