using Core.MessengerStatic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _button;
        private Location _location;
        public void Setup(Location location)
        {
            _title.text = location.ID;
            _location = location;
        
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Messenger<Location>.Broadcast(Constnts.Events.Game.SelectLocation, _location);
        }
    }
}
