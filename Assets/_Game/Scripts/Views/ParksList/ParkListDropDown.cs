using OurWorld.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    [RequireComponent(typeof(Dropdown))]
    public class ParkListDropDown : MonoBehaviour
    {
        [SerializeField] private NearbyPlacesListElement _element;
        [SerializeField] private NearbyPlacesController _nearbyPlacesController;

        private Dropdown _dropDown;

        private void Awake()
        {
            _dropDown = GetComponent<Dropdown>();
        }

        private void OnEnable()
        {
            _dropDown.onValueChanged.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            _dropDown.onValueChanged.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick(int value)
        {
            if (!_element.Active)
            {
                _element.OnClosedEvent += OnParkListElementClosed;
                _element.Open();
                _nearbyPlacesController.searchNewPlacesNearbyAndClearOld(_dropDown.options[value].text);
                gameObject.SetActive(false);
            }
        }

        private void OnParkListElementClosed()
        {
            _element.OnClosedEvent -= OnParkListElementClosed;
            gameObject.SetActive(true);
        }
    }
}