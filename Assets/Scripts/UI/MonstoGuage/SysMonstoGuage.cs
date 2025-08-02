using System;
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

        private bool _ispaused = false;

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
                .OnUpdate(() =>
                {
                    _cmpMonstoGuage.ProgressForUI = en.Slider.value / en.Slider.maxValue;
                    var color = en.Gradient.Evaluate(_cmpMonstoGuage.ProgressForUI);
                    en.FillImage.color = color;
                })
                .OnComplete(() =>
                {
                    en.Slider.value = 0;
                    _guageUpTween.Restart();
                })
                .Play();

            var input = TipsyPlayerInput.Instance;
            input.OnStartDrag.Subscribe(_ =>
            {
                if (_ispaused) return;

                _allowGuageUp = true;
            });
            input.OnEndDrag.Subscribe(_ =>
            {
                _allowGuageUp = false;
                _cmpMonstoGuage.Progress = en.Slider.value;
                en.Slider.value = 0;
                _guageUpTween.Rewind();
            });

            GetEntity<EnMonstoGuage>().World.OnPaused += OnPaused;
            GetEntity<EnMonstoGuage>().World.OnResume += OnResume;
        }

        private void OnDestroy()
        {
            GetEntity<EnMonstoGuage>().World.OnPaused -= OnPaused;
            GetEntity<EnMonstoGuage>().World.OnResume -= OnResume;
        }

        private void OnResume()
        {
            _ispaused = false;
        }

        private void OnPaused()
        {
            _ispaused = true;
        }

        private void Update()
        {
            if (_allowGuageUp)
            {
                _guageUpTween.ManualUpdate(
                    PlayerTime.DeltaTime * PlayerTime.TimeScale,
                    PlayerTime.UnscaledDeltaTime);
            }
        }
    }
}