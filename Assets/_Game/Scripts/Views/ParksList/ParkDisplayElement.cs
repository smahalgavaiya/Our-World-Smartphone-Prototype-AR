using Mapbox.Utils;
using OurWorld.Scripts.DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    public class ParkDisplayElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _placeNameText, _distanceText;
        [SerializeField] private Button _interactionButton;

        private ParkData _data;

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

        public void Initialize(ParkData data)
        {
            _data = data;
        }

        private void SetVisuals(ParkData data)
        {
            _placeNameText.text = data.Name;
        }
        private void OnButtonPressed() { }
    }
}