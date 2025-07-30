using UnityEngine;

namespace ImTipsyDude.InstantECS
{
    /// <summary>
    /// NOTE:
    /// コンポーネントにはデータのみ集めておく。依存関係は集めない。
    /// </summary>
    [DefaultExecutionOrder((int)ExecutionOrder.EntityOrComponent)]
    public class IECSComponent : MonoBehaviour
    {
        public int ID => GetInstanceID();

        public virtual void OnStart()
        {
        }

        public IECSEntity Entity => gameObject.GetComponent<IECSEntity>();

        private void Start()
        {
            IECSWorld.GetScene().BindComponent(this);
            OnStart();
        }
    }
}