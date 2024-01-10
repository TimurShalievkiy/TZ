using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class MemberCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _selectedMarker;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        private MemberData _member;
        private bool _isSelected;

        public void Setup(MemberData member)
        {
            ChangeSelection(false);
            _member = member;
            _name.text = _member.Member.AddressableKey;
            SetIcon();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ChangeSelection(!_isSelected);
            Messenger<MemberData>.Broadcast(Constnts.Events.Game.MemberClick, _member);
        }

        private void ChangeSelection(bool value)
        {
            _isSelected = value;
            _selectedMarker.enabled = _isSelected;
        }

        private async void SetIcon()
        {
            _icon.sprite = await AssetProvider.LoadAssetAsync<Sprite>($"{_member.Member.AddressableKey}_icon");
        }
    }
}