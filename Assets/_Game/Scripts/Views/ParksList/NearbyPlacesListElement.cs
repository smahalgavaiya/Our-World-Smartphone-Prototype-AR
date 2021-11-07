using System;
using System.Collections.Generic;
using DG.Tweening;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;
using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    public class NearbyPlacesListElement : MonoBehaviour
    {
        public event Action OnOpenedEvent;
        public event Action OnClosedEvent;

        [SerializeField] private PlaceDisplayElement _elementPrefab;

        [SerializeField] private ScrollRect _targetScrollRect;

        [SerializeField] private Button _closeButton;

        [Space, SerializeField] private AnimationProperties _animationProperties;

        private Dictionary<IPointOfInterest, PlaceDisplayElement> _elementsDictionary = new Dictionary<IPointOfInterest, PlaceDisplayElement>();

        private Tween _panelTween;

        private bool _active;

        private Action<IPointOfInterest> _onPlaceSelected;

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

        public void Initialize(List<IPointOfInterest> nearbyParks, Action<IPointOfInterest> onPlaceSelected)
        {
            _onPlaceSelected = onPlaceSelected;
            foreach (var parkData in nearbyParks)
            {
                var element = Instantiate(_elementPrefab, _targetScrollRect.content);

                element.Initialize(parkData, OnPlaceButtonClick);

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
        private void OnPlaceButtonClick(IPointOfInterest elementData)
        {
            _onPlaceSelected?.Invoke(elementData);
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