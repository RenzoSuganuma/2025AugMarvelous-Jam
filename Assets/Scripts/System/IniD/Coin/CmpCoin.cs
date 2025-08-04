using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class CmpCoin : IECSComponent
{
    public int Score;
    public float IncreaseInSpeed = 1000f;
    public float NockUpSpeed = 10f;
    public float DestoroyDuration = 5f;
}
