using System;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;
using TMPro;
using UnityEngine;

using OurWorld.Scripts.Navigation.Directions;
using OurWorld.Scripts.Providers.MapAPIProviders;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    public class PlaceDisplayElement : MonoBehaviour
    {

        [SerializeField] private TMP_Text _placeNameText, _distanceText,_timeText;
        [SerializeField] private Button _interactionButton;


        [SerializeField] private DirectionsController TargetScript;

        private POIData _data;

        private Action<POIData> _onClickAction;


        #region Engine Methods
        private void OnEnable()
        {
            _interactionButton.onClick.AddListener(OnButtonPressed);

            TargetScript = GameObject.FindObjectOfType<DirectionsController>();

            TargetScript.Initialize(new MapboxDirectionsAPIProvider());
        }
        private void OnDisable()
        {
            _interactionButton.onClick.RemoveListener(OnButtonPressed);
        }
        #endregion

        public void Initialize(IPointOfInterest data, Action<IPointOfInterest> onClickAction)
        {
            _data = data as POIData;
            _onClickAction = onClickAction;
            SetVisuals(_data);
        }
        private void SetVisuals(POIData data)
        {
            _placeNameText.text = data.Name;
            var distanceText = data.Distance < 1 ? $"{(data.Distance * 1000):F0}m" : $"{data.Distance:F2}Km"; 
            _distanceText.text = distanceText;
            float time = data.Distance / 5;//General Walking speed of humans 
            var timeText = time < 1 ? $"{(time * 60):F0} min" : $"{time:F2} Hours";
            _timeText.text = timeText;
        }
        private void OnButtonPressed()
        {
            _onClickAction?.Invoke(_data);

            Debug.Log("button pressed"+ _data.Geolocation);
            if (TargetScript.IsNavigating)
            {
                Debug.Log("Disposed called and new nav");
                TargetScript.Dispose();
                TargetScript.CreateDirectionsForTarget(_data.Geolocation);
            }
            else
            {
                Debug.Log("New navigation called");
                TargetScript.CreateDirectionsForTarget(_data.Geolocation);
            }
        }
    }
}