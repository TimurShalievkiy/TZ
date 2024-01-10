using Core.MessengerStatic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudActionButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _button;

    private Ability _ability;

    public void Setup(Ability ability)
    {
        _ability = ability;
        SetIcon();
        _button.onClick.AddListener(OnClick);
    }

    public void UpdateState(int playerPoints)
    {
        _button.interactable = false;
        if (!_ability.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var points))
        {
            return;
        }

        _button.interactable = points.Value <= playerPoints;

        _title.text = points.Value.ToString();
    }
    
    private async void SetIcon()
    {
        _icon.sprite = await AssetProvider.LoadAssetAsync<Sprite>($"{_ability.ID}_icon");
    }

    private void OnClick()
    {
        Messenger<Ability>.Broadcast(Constnts.Events.Game.ActionClick, _ability);
    }
}