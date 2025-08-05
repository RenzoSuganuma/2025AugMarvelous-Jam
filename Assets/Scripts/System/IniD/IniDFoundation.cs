using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniDFoundation : MonoBehaviour
{
    private void Start()
    {
#if UNITY_STANDALONE
        SceneManager.LoadScene("Entry", LoadSceneMode.Additive);
#endif
    }
}