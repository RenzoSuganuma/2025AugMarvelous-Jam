using DG.Tweening;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using R3;
using TMPro;
using UnityEngine;

namespace ImTipsyDude.Scene
{
    public class Level1SceneEntity : SceneEntity
    {
        [SerializeField] private TMP_Text _countDownText;
        
        public Subject<Unit> OnStartGame = new Subject<Unit>();

        public override void OnStart()
        {
            IECSWorld.Instance.StartLoadNextScene("Exit");
            IECSWorld.Instance.UpdateInGameState(InGameState.Waiting);

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    SysSoundManager.Instance.PlaySE("se_start");
                    _countDownText.rectTransform.DOPunchPosition(Random.insideUnitCircle * 5, 0.5f);
                    _countDownText.text = "3";

                    Debug.Log("3");
                })
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _countDownText.rectTransform.DOPunchPosition(Random.insideUnitCircle * 7, 0.5f);
                    _countDownText.text = "2";
                    Debug.Log("2");
                })
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _countDownText.rectTransform.DOPunchPosition(Random.insideUnitCircle * 10, 0.5f);
                    _countDownText.text = "1";
                    SysSoundManager.Instance.PlaySE("SE_Engine");
                    Debug.Log("1");
                })
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _countDownText.text = string.Empty;
                    IECSWorld.Instance.UpdateInGameState(InGameState.Playing);
                    OnStartGame.OnNext(Unit.Default);
                })
                .Play();
        }

        public void OnTimeOut()
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _countDownText.rectTransform.DOPunchPosition(Random.insideUnitCircle * 5, 0.5f);
                    _countDownText.text = "TIME OUT!";
                })
                .AppendInterval(3)
                .AppendCallback(() => IECSWorld.Instance.UnLoadScene("Level1", o => { }))
                .Play();
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnTerminate()
        {
            EnInstanceIdPool.Instance.Map.Clear();
        }
    }
}