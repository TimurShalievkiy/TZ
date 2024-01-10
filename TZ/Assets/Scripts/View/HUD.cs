using System.Collections.Generic;
using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using View;

public class HUD : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private HUDMembersView _prefab;

    [SerializeField] private Transform _rootActions;
    [SerializeField] private HudActionButton _prefabActions;
    [SerializeField] private Button _nextTurnButton;
    
    private List<HUDMembersView> _members;
    private List<HudActionButton> _actionButtons;
    private MemberData _memberData;
    void Start()
    {
        _members = new List<HUDMembersView>();
        _actionButtons = new List<HudActionButton>();

        Messenger<GameController.GameState>.AddListener(Constnts.Events.Game.ChangeGameState, ChangeTurn);
        Messenger<MemberData>.AddListener(Constnts.Events.Game.HUDMemberClick, OnMemberSelected);
        Messenger<List<Team>>.AddListener(Constnts.Events.GameField.CreateTeams, OnTeamsCreate);
        Messenger<MemberData>.AddListener(Constnts.Events.Game.MemberUpdate, MemberUpdate);
        
        Messenger<Ability>.AddListener(Constnts.Events.Game.ActionClick, EmptyActionClick);
        
        _nextTurnButton.onClick.AddListener(OnNextTurnClick);
        gameObject.SetActive(false);
    }

    private void EmptyActionClick(Ability ability)
    {
        if (string.IsNullOrEmpty(ability.ID))
        {
            if (_memberData.Member.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var points))
            {
                foreach (var actionButton in _actionButtons)
                {
                    actionButton.UpdateState(points.Value);
                }
            }
        }
    }

    private void MemberUpdate(MemberData obj)
    {
        var member = _members.FirstOrDefault(x => x.IsSameMember(obj));
        if (member != null)
        {
            member.UpdateView();
        }
    }

    private void OnTeamsCreate(List<Team> teams)
    {
        foreach (var team in teams)
        {
            if (team.Relationship != Team.RelationshipType.Player)
            {
                continue;
            }

            foreach (var member in team.MembersData)
            {
                var newMember = Instantiate(_prefab, _root);
                newMember.Setup(member);
                _members.Add(newMember);
            }
        }
    }

    private void UpdateMembers()
    {
        foreach (var member in _members)
        {
            member.UpdateView();
        }

        OnMemberSelected(null);
    }

    private void OnMemberSelected(MemberData memberData)
    {
        _memberData = memberData;
        
        foreach (var action in _actionButtons)
        {
            action.gameObject.SetActive(false);
            Destroy(action.gameObject, 1);
        }
        
        _actionButtons.Clear();
        

        if (memberData == null)
        {
            foreach (var member in _members)
            {
                member.ChangeSelection(false);
            }

            return;
        }

        if (!_memberData.Member.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var points))
        {
            return;
        }

        foreach (var member in _members)
        {
            member.ChangeSelection(member.IsSameMember(memberData));

            if (!member.IsSameMember(memberData))
            {
                continue;
            }

            foreach (var ability in memberData.Member.Abilities)
            {
                var newAbility = Instantiate(_prefabActions, _rootActions);
                newAbility.Setup(ability);
                newAbility.UpdateState(points.Value);
                _actionButtons.Add(newAbility);
            }
        }
    }

    private void OnNextTurnClick()
    {
        Messenger.Broadcast(Constnts.Events.Game.NextTurn);
    }

    private void ChangeTurn(GameController.GameState obj)
    {
        UpdateMembers();
        switch (obj)
        {
            case GameController.GameState.None:
                gameObject.SetActive(false);
                break;
            case GameController.GameState.PlayerTurn:
                gameObject.SetActive(true);
                break;
            case GameController.GameState.GameTurn:
                gameObject.SetActive(false);
                break;
        }
    }
}