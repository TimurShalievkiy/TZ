using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class HUDMembersView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectedMarker;
        [SerializeField] private TMP_Text _actionPoints;
        
        private MemberData _member;
        private bool _isSelected;

        public void Setup(MemberData member)
        {
            ChangeSelection(false);
            _member = member;
            SetPoints();
            SetIcon();
        }

        private async void SetIcon()
        {
            _icon.sprite = await AssetProvider.LoadAssetAsync<Sprite>($"{_member.Member.AddressableKey}_icon");
        }
        private void SetPoints()
        {
            if (!_member.Member.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var points))
            {
                return;
            }
            
            _actionPoints.text = points.Value.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ChangeSelection(!_isSelected);
            Messenger<MemberData>.Broadcast(Constnts.Events.Game.HUDMemberClick, _member);
        }

        public void ChangeSelection(bool value)
        {
            _isSelected = value;
            _selectedMarker.enabled = _isSelected;
        }

        public bool IsSameMember(MemberData member)
        {
            return member.Member.ID.Equals(_member.Member.ID);
        }

        public void UpdateView()
        {
            SetPoints();
        }
    }
}