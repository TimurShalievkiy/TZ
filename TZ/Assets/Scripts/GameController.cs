using System.Collections.Generic;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameState _gameState;
    
    public enum GameState
    {
        None,
        PlayerTurn,
        GameTurn,
    }

    public static GameState CurrentGameState
    {
        get => _gameState;
        private set => _gameState = value;
    }

    [SerializeField] private UIManager _uiManager;
    
    private BattleController _battleController;
    private TestData _gameData;
    private List<MemberData> _selectedPlayerCharacters;
    private Location _currentSelectedLocation;
    private AbilityController _abilityController;
    public void Start()
    {
        _battleController = new BattleController();
        _abilityController = new AbilityController();
        
        CurrentGameState = GameState.None;

        _gameData = new TestData();
        _gameData.InitCharactersDatabase();
        _gameData.InitLocationDatabase();
        _gameData.InitAvailableMembersDatabase();
        _uiManager.Setup(_gameData);
        _currentSelectedLocation = _uiManager.GetCurrentSelectedLocation();
        _selectedPlayerCharacters = new List<MemberData>();

        Messenger<MemberData>.AddListener(Constnts.Events.Game.SelectMember, SelectMember);
        Messenger<MemberData>.AddListener(Constnts.Events.Game.DeselectMember, DeselectMember);
        Messenger<Location>.AddListener(Constnts.Events.Game.SelectLocation, SelectLocation);
        Messenger<GameState>.AddListener(Constnts.Events.Game.ChangeGameState, ChangeGameState);
        Messenger.AddListener(Constnts.Events.Game.StartGame, OnGameStart);
    }

    private void ChangeGameState(GameState state)
    {
        CurrentGameState = state;
        Debug.LogError(CurrentGameState);
    }

    private void OnGameStart()
    {
        if (_currentSelectedLocation == null)
        {
            return;
        }

        _battleController.Setup(new Team() { MembersData = _selectedPlayerCharacters, Relationship = Team.RelationshipType.Player }, _currentSelectedLocation);
    }

    private void SelectMember(MemberData member)
    {
        _selectedPlayerCharacters.Add(member);
    }

    private void DeselectMember(MemberData member)
    {
        _selectedPlayerCharacters.Remove(member);
    }

    private void SelectLocation(Location location)
    {
        _currentSelectedLocation = location;
        Debug.LogError(location.ID);
    }

    private void OnDestroy()
    {
        Messenger<MemberData>.RemoveListener(Constnts.Events.Game.SelectMember, SelectMember);
        Messenger<MemberData>.RemoveListener(Constnts.Events.Game.DeselectMember, DeselectMember);
        Messenger<Location>.RemoveListener(Constnts.Events.Game.SelectLocation, SelectLocation);
        Messenger.RemoveListener(Constnts.Events.Game.StartGame, OnGameStart);
    }
}