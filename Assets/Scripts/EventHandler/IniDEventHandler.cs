using System;
using UnityEngine;

namespace ImTipsyDude.IniD.Event
{
    public class IniDEventHandler : MonoBehaviour
    {
        public Action<Vector2> OnSlide;
        public Action OnPlayerJump;
        public Action OnPlayerCollide;
        public Action OnTimeOut;
        
        
        public static IniDEventHandler Instance;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}