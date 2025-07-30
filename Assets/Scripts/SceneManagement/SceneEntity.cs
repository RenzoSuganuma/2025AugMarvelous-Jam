using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using ImTipsyDude.BolaBoom;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace ImTipsyDude.Scene
{
    [DefaultExecutionOrder((int)ExecutionOrder.Scene)]
    public abstract class SceneEntity : MonoBehaviour
    {
        private Dictionary<int, IECSComponent> _components;
        private Dictionary<int, IECSSystem> _systems;

        public AsyncOperation AsyncOperation { get; set; }

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void OnTerminate();

        public void BindComponent(IECSComponent component)
            => _components.Add(component.ID, component);

        public void BindSystem(IECSSystem system)
            => _systems.Add(system.ID, system);

        public void PullComponent<T>(int componentId, out T component) where T : IECSComponent
        {
            component = _components[componentId] as T;
        }

        public void PullSystem<T>(int systemId, out T system) where T : IECSSystem
        {
            system = _systems[systemId] as T;
        }

        private void Start()
        {
            _components = new();
            _systems = new();
            IECSWorld.Instance.UpdateCurrentScene(this);
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        private void OnDestroy()
        {
            _components?.Clear();
            _systems?.Clear();
            AsyncOperation = null;
            OnTerminate();
        }
    }
}