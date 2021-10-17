using System;
using System.Collections.Generic;
using DG.Tweening;
using OurWorld.Scripts.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    public class ParkListElement : MonoBehaviour
    {
        public event Action OnOpenedEvent;
        public event Action OnClosedEvent;

        [SerializeField] private ParkDisplayElement _elementPrefab;

        [SerializeField] private ScrollRect _targetScrollRect;

        [SerializeField] private Button _closeButton;

        [Space, SerializeField] private AnimationProperties _animationProperties;

        private Dictionary<ParkData, ParkDisplayElement> _elementsDictionary = new Dictionary<ParkData, ParkDisplayElement>();

        private Tween _panelTween;

        private bool _active;

        private Action<ParkData> _onParkSelected;

        private RectTransform RectTransform => transform as RectTransform;

        public bool Active => _active;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
        }
        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Initialize(List<ParkData> nearbyParks, Action<ParkData> onParkSelected)
        {
            _onParkSelected = onParkSelected;
            foreach (var parkData in nearbyParks)
            {
                var element = Instantiate(_elementPrefab, _targetScrollRect.content);

                element.Initialize(parkData, OnParkButtonClick);

                _elementsDictionary.Add(parkData, element);
            }
        }

        public void Open()
        {
            Debug.Log("Opening Panel");

            OpenAnimation(OnOpened);

            void OnOpened()
            {
                _active = true;
                OnOpenedEvent?.Invoke();
            }
        }
        public void Close()
        {
            Debug.Log("Closing Panel");

            CloseAnimation(OnClosed);

            void OnClosed()
            {
                _active = false;
                OnClosedEvent?.Invoke();
            }
        }
        public void ToggleLoading(bool toggle)
        {

        }
        private void OnParkButtonClick(ParkData elementData)
        {
            _onParkSelected?.Invoke(elementData);
        }

        #region Panel Animations
        private void OpenAnimation(Action onComplete = null)
        {
            if (DOTween.IsTweening(_panelTween)) return;

            gameObject.SetActive(true);

            _panelTween = RectTransform.DOAnchorPos(_animationProperties.OpenedPosition, _animationProperties.OpeningTime)
            .SetEase(_animationProperties.OpeningEase).OnComplete(() => onComplete?.Invoke());
        }

        private void CloseAnimation(Action onComplete = null)
        {
            if (DOTween.IsTweening(_panelTween)) return;

            _panelTween = RectTransform.DOAnchorPos(_animationProperties.ClosedPosition, _animationProperties.ClosingTime)
            .SetEase(_animationProperties.ClosingEase).OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }

        #endregion
        
        [Serializable]
        private class AnimationProperties
        {
            public float OpeningTime, ClosingTime;
            public Ease OpeningEase, ClosingEase;

            public Vector3 OpenedPosition, ClosedPosition;
        }
    }
}