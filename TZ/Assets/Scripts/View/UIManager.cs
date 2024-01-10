using System.Collections.Generic;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _hud;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private Transform _levelButtonPrefabRoot;
    [SerializeField] private Button _playButton;

    [SerializeField] private TMP_Text _counterOfMembers;
    [SerializeField] private TMP_Text _selectedLocation;
    [SerializeField] private MemberCardView _memberCardViewPrefab;
    [SerializeField] private Transform _membersRoot;
    [SerializeField] private List<MemberCardView> _memberCardViews;

    private List<MemberData> _selectedCharacters;
    private Location _currentSelectedLocation;

    public void Setup(TestData data)
    {
        Messenger<MemberData>.AddListener(Constnts.Events.Game.MemberClick, OnMemberClick);
        Messenger<Location>.AddListener(Constnts.Events.Game.SelectLocation, SelectLocation);

        _selectedCharacters = new List<MemberData>();
        _memberCardViews = new List<MemberCardView>();

        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(StartBattle);

        if (_currentSelectedLocation == null)
        {
            SelectLocation(data.LocationsDatabase[0]);
        }
        
        foreach (var location in data.LocationsDatabase)
        {
            var levelButton = Instantiate(_levelButtonPrefab, _levelButtonPrefabRoot);
            levelButton.Setup(location);
        }

        foreach (var character in data.AvailableMembers)
        {
            var member = Instantiate(_memberCardViewPrefab, _membersRoot);
            member.Setup(character);
            _memberCardViews.Add(member);
        }

        CheckPlayButton();
    }

    private void SelectLocation(Location location)
    {
        _currentSelectedLocation = location;
        _selectedLocation.text = $"selected location: {location.ID}";
        CheckPlayButton();
    }

    private void OnMemberClick(MemberData member)
    {
        _selectedCharacters ??= new List<MemberData>();

        if (_selectedCharacters.Contains(member))
        {
            _selectedCharacters.Remove(member);
            Messenger<MemberData>.Broadcast(Constnts.Events.Game.DeselectMember, member);
        }
        else
        {
            _selectedCharacters.Add(member);
            Messenger<MemberData>.Broadcast(Constnts.Events.Game.SelectMember, member);
        }

        CheckPlayButton();
    }

    private void CheckPlayButton()
    {
        _counterOfMembers.text = $"{_selectedCharacters.Count}/{_currentSelectedLocation.PlayerMaxMemberCount}";
        _playButton.interactable = _currentSelectedLocation != null && _selectedCharacters.Count >= 1 && _selectedCharacters.Count <= _currentSelectedLocation.PlayerMaxMemberCount;
    }

    private void StartBattle()
    {
        Messenger.Broadcast(Constnts.Events.Game.StartGame);
        _mainScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        Messenger<MemberData>.RemoveListener(Constnts.Events.Game.MemberClick, OnMemberClick);
        Messenger<Location>.RemoveListener(Constnts.Events.Game.SelectLocation, SelectLocation);
    }

    public Location GetCurrentSelectedLocation()
    {
        return _currentSelectedLocation;
    }
}