using System.Collections.Generic;
using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

public class BattleController
{
    private Location _location;
    private GameField _gameField;
    private int _currentTeamIndex;
    private List<Team> _teams;

    public void Setup(Team playerTeam, Location location)
    {
        _gameField ??= new GameField();
        _gameField.ClearGameField();

        _location = location;
        _gameField.Setup(_location);

        CreateTeam(playerTeam);

        Messenger<List<Team>>.Broadcast(Constnts.Events.GameField.CreateTeams, _teams);
        Messenger.AddListener(Constnts.Events.Game.NextTurn, NextTurn);

        _currentTeamIndex = -1;
        NextTurn();
    }

    private void NextTurn()
    {
        var nextTeam = GetNextTeam();
        if (nextTeam == null)
        {
            Messenger<GameController.GameState>.Broadcast(Constnts.Events.Game.ChangeGameState, GameController.GameState.None);
            return;
        }

        switch (nextTeam.Relationship)
        {
            case Team.RelationshipType.Player:
                UpdateTeamsPoints();
                Messenger<GameController.GameState>.Broadcast(Constnts.Events.Game.ChangeGameState, GameController.GameState.PlayerTurn);
                break;
            case Team.RelationshipType.Enemy:
                UpdateTeamsPoints();
                Messenger<GameController.GameState>.Broadcast(Constnts.Events.Game.ChangeGameState, GameController.GameState.GameTurn);
                break;
        }
    }

    private void UpdateTeamsPoints()
    {
        foreach (var team in _teams)
        {
            foreach (var member in team.MembersData)
            {
                var points = member.Member.Stats[Constnts.Stats.ActionPoints];
                points.Value = member.Member.Stats[Constnts.Stats.MaxActionPoints].Value;
                member.Member.Stats[Constnts.Stats.ActionPoints] = points;
            }
        }
    }

    private Team GetNextTeam()
    {
        int countOfTeamsWithMembers = _teams.Count(x => x.MembersData.Count > 0);
        if (countOfTeamsWithMembers == 1)
        {
            var winTeam = _teams.FirstOrDefault(x => x.MembersData.Count > 0);
            Debug.LogError("win " + winTeam.Relationship);
            return null;
        }

        _currentTeamIndex = GetNextTeamIndex();
        return _teams[_currentTeamIndex];
    }

    private int GetNextTeamIndex()
    {
        //remove this for next team
        _currentTeamIndex = -1;
        ////////////
        int checkCounter = _teams.Count;
        int index = _currentTeamIndex;
        while (checkCounter >= 0)
        {
            index++;
            if (index >= _teams.Count)
            {
                index = 0;
            }

            if (_teams[index].MembersData.Count != 0)
            {
                break;
            }

            checkCounter--;
        }

        return index;
    }

    private void CreateTeam(Team playerTeam)
    {
        int idIndex = 0;
        _teams = new List<Team>();

        var newPlayerTeam = new Team
        {
            Relationship = playerTeam.Relationship,
            MembersData = new List<MemberData>()
        };

        for (var index = 0; index < playerTeam.MembersData.Count; index++)
        {
            var memberData = playerTeam.MembersData[index];
            if (_location.PlayerTeamPlaces.Count > index)
            {
                memberData.WorldPosition = _location.PlayerTeamPlaces[index];
            }

            memberData.Member.ID = idIndex.ToString();
            idIndex++;

            newPlayerTeam.MembersData.Add(new MemberData(memberData));
        }

        _teams.Add(newPlayerTeam);

        foreach (var team in _location.Teams)
        {
            var newTeam = new Team
            {
                Relationship = team.Relationship,
                MembersData = new List<MemberData>()
            };

            foreach (var member in team.MembersData)
            {
                member.Member.ID = idIndex.ToString();
                idIndex++;
                newTeam.MembersData.Add(new MemberData(member));
            }

            _teams.Add(newTeam);
        }
    }
}