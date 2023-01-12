using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace OurWorld.Scripts.Utilities
{
    public class CrossfadeTransition : MonoBehaviour
    {
        private Tween _colorTween;
        private Image _targetImage;

        private Image TargetImage
        {
            get
            {
                if (!_targetImage) _targetImage = GetComponent<Image>();
                return _targetImage;
            }
        }
        public void FadeIn(Action onComplete)
        {
            KillTweenIfItsActive();

            var targetColor = TargetImage.color;
            targetColor.a = 1;

            TargetImage.color *= new Color(1, 1, 1, 0);

            gameObject.SetActive(true);

            TargetImage.DOColor(targetColor, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(
                () =>
                {
                    onComplete?.Invoke();
                }
            );
        }

        public void FadeOut(Action onComplete)
        {
            KillTweenIfItsActive();

            var targetColor = TargetImage.color * new Color(1, 1, 1, 0);

            TargetImage.color += new Color(0, 0, 0, 1);

            TargetImage.DOColor(targetColor, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(
                () =>
                {
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                }
            );
        }

        private void KillTweenIfItsActive()
        {
            if (DOTween.IsTweening(_colorTween))
            {
                DOTween.Kill(_colorTween, true);
            }
        }

        #region Singleton
        private static CrossfadeTransition _instance;

        public static CrossfadeTransition Instance => _instance;

        private void Awake()
        {
            if (_instance && _instance != this)
                Destroy(gameObject);
            _instance = this;
        }
        #endregion
    }
}
