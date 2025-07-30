using Cysharp.Threading.Tasks;
using DG.Tweening;
using ImTipsyDude.InstantECS;
using ImTipsyDude.Player;
using ImTipsyDude.Tween;
using R3;
using UnityEngine;

namespace ImTipsyDude.BolaBoom
{
    public class SysMonstoGuage : IECSSystem
    {
        private CmpMonstoGuage _cmpMonstoGuage;
        private DG.Tweening.Tween _guageUpTween;
        private bool _allowGuageUp = false;
        private float _eTime = 0;

        public override void OnStart()
        {
            _cmpMonstoGuage = GetComponent<CmpMonstoGuage>();
            var en = GetEntity<EnMonstoGuage>();
            en.Slider.maxValue = _cmpMonstoGuage.GuageMaxValue;
            en.Slider.value = 0;

            _guageUpTween = DOTween.To(() => en.Slider.value,
                    value => { en.Slider.value = value; },
                    _cmpMonstoGuage.GuageMaxValue,
                    _cmpMonstoGuage.DurationToBeMax)
                .SetDelay(_cmpMonstoGuage.DelayToStartGuageUp)
                .SetUpdate(UpdateType.Manual)
                .OnComplete(() =>
                {
                    en.Slider.value = 0;
                    _guageUpTween.Restart();
                })
                .Play();

            var input = TipsyPlayerInput.Instance;
            input.OnStartDrag.Subscribe(_ => { _allowGuageUp = true; });
            input.OnEndDrag.Subscribe(_ =>
            {
                _allowGuageUp = false;
                _cmpMonstoGuage.Progress = en.Slider.value;
                en.Slider.value = 0;
                _guageUpTween.Restart();
            });
        }

        private void Update()
        {
            if (_allowGuageUp)
            {
                _guageUpTween.ManualUpdate(PlayerTime.DeltaTime, PlayerTime.UnscaledDeltaTime);
            }
        }
    }
}