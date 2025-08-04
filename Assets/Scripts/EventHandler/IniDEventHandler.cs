using System;
using UnityEngine;

namespace ImTipsyDude.IniD.Event
{
    public class IniDEventHandler : MonoBehaviour
    {
        public Action<Vector2> OnSlide;
        public Action OnPlayerJump;
        public Action OnPlayerCollide;
    }
}