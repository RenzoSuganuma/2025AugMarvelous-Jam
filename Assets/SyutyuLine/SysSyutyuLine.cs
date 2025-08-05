using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysSyutyuLine : IECSSystem
{
    private CmpSyutyuLine _cmpSyutyuLine;
    private EnSyutyuLine _enSyutyuLine;
    public override void OnStart()
    {
        _cmpSyutyuLine = GetComponent<CmpSyutyuLine>();
        _enSyutyuLine = GetComponent<EnSyutyuLine>();
    }
    public void Syutyu()
    {
        _enSyutyuLine.RectT.DOScale(new Vector3(_cmpSyutyuLine.SyutyuScale,_cmpSyutyuLine.SyutyuScale,_cmpSyutyuLine.SyutyuScale), _cmpSyutyuLine.SyutyuTime)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine); 
    }
}
