using System;
using UnityEngine;

public class GlobalEventPool : MonoBehaviour
{
    public Action<Vector2> OnSlide;
    public Action OnPlayerJump;
    public Action OnPlayerCollide;
}