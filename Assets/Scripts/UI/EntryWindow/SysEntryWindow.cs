using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using R3;
using TMPro;
using UnityEngine;

public class SysEntryWindow : IECSSystem
{
    public void EnterInGame()
    {
        GetEntity<EnEntryWindow>().World.UnLoadScene("Entry", o => { });
    }

    public void QuitGame()
    {
        GetEntity<EnEntryWindow>().World.QuitGame();
    }

    public override void OnStart()
    {
        var c = GetComponent<CmpEntryWindow>();
        if (c == null) return;
        foreach (var t in c.Texts)
        {
            t.font = EnDependencyPool.Instance.FontAsset as TMP_FontAsset;
        }

        var entity = GetEntity<EnEntryWindow>();
        entity.TitleLogo.transform
            .DOScale(Vector3.one * 1.5f, 1)
            .SetLoops(-1, LoopType.Yoyo);
    }
}