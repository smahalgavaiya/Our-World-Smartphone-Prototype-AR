using System;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    public class PlaceDisplayElement : MonoBehaviour
    {

        [SerializeField] private TMP_Text _placeNameText, _distanceText;
        [SerializeField] private Button _interactionButton;

        private POIData _data;

        private Action<POIData> _onClickAction;


        #region Engine Methods
        private void OnEnable()
        {
            _interactionButton.onClick.AddListener(OnButtonPressed);
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
        }
        private void OnButtonPressed()
        {
            _onClickAction?.Invoke(_data);
        }
    }
}