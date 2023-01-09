using UnityEngine;
using UnityEngine.UI;

namespace OurWorld.Scripts.Views.ParksList
{
    [RequireComponent(typeof(Button))]
    public class ParkListToggleButton : MonoBehaviour
    {
        [SerializeField] private NearbyPlacesListElement _element;

        private Button _toggleButton;

        private void Awake()
        {
            _toggleButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (!_element.Active)
            {
                _element.OnClosedEvent += OnParkListElementClosed;
                _element.Open();
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