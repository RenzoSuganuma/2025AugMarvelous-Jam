using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.InstantECS;
using UnityEngine;
using UnityEngine.Serialization;

public class CmpCoin : IECSComponent
{
    public int Score;
    public bool WillSpeedUp = true;
    public float IncreaseInSpeed = 1000f;
    public float NockUpSpeed = 10f;
    public int RotateTimesPerSec = 3;
    public float DestoroyDuration = 5f;
}
