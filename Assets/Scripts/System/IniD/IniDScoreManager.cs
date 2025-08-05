using System;
using UnityEngine;

public class IniDScoreManager : MonoBehaviour
{
    public static IniDScoreManager Instance;

    public int GotCoin { get; set; }
    public int Score { get; set; }
    public float RemainingTime { get; set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}