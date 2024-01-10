using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class MoveAbility
    {
        private MemberData _selectedMember;
        private Ability _ability;
        private List<WorldBox> _worldBoxes;
        private bool _isActive;
        private List<Team> _teamas;

        public MoveAbility()
        {
            Messenger<List<WorldBox>>.AddListener(Constnts.Events.GameField.NewLevelCreated, OnLevelCreated);
            Messenger<MemberData>.AddListener(Constnts.Events.Game.HUDMemberClick, SelectMember);
            Messenger<List<Team>>.AddListener(Constnts.Events.GameField.CreateTeams, OnTeamsCreated);

            Messenger<WorldBox>.AddListener(Constnts.Events.Game.BoxClick, OnBoxClick);
        }

        private void OnTeamsCreated(List<Team> teams)
        {
            _teamas = teams;
        }

        private void OnBoxClick(WorldBox obj)
        {
            if (!_isActive)
            {
                return;
            }

            if (obj.BoxState != WorldBox.State.Interactive)
            {
                return;
            }

            if (!_ability.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var points))
            {
                return;
            }

            if (!_selectedMember.Member.Stats.TryGetValue(Constnts.Stats.ActionPoints, out var playerPoints))
            {
                return;
            }

            var memberStat = _selectedMember.Member.Stats[Constnts.Stats.ActionPoints];
            memberStat.Value -= points.Value;
            _selectedMember.Member.Stats[Constnts.Stats.ActionPoints] = memberStat;
            _selectedMember.WorldPosition = obj.WorldPosition;

            Messenger<MemberData>.Broadcast(Constnts.Events.Game.MemberUpdate, _selectedMember);
            Messenger<Ability>.Broadcast(Constnts.Events.Game.ActionClick, new Ability() { ID = String.Empty });
        }

        private void SelectMember(MemberData obj)
        {
            _selectedMember = obj;
        }

        private void OnLevelCreated(List<WorldBox> obj)
        {
            _worldBoxes = obj;
        }

        public void SetAbility(Ability ability)
        {
            _ability = ability;
        }

        public void SetSelection(bool value)
        {
            _isActive = value;
            
            if (_isActive)
            {
                MarkAvailableBoxes();
            }
        }

        private void MarkAvailableBoxes()
        {
            if (!_ability.Stats.TryGetValue(Constnts.Stats.Distance, out var speed))
            {
                return;
            }

            int minI = _selectedMember.WorldPosition.X - speed.Value;
            int maxI = _selectedMember.WorldPosition.X + speed.Value;

            int minJ = _selectedMember.WorldPosition.Y - speed.Value;
            int maxJ = _selectedMember.WorldPosition.Y + speed.Value;

            var listOfSelectedBoxes = _worldBoxes.Where(x => x.WorldPosition.X >= minI && x.WorldPosition.X <= maxI &&
                                                             x.WorldPosition.Y >= minJ && x.WorldPosition.Y <= maxJ).ToList();

            foreach (var team in _teamas)
            {
                foreach (var member in team.MembersData)
                {
                    var box = listOfSelectedBoxes.FirstOrDefault(x => x.WorldPosition == member.WorldPosition);
                    if (box != null)
                    {
                        listOfSelectedBoxes.Remove(box);
                    }
                }
            }
            
            foreach (var worldBox in listOfSelectedBoxes)
            {
                if (worldBox.BoxState == WorldBox.State.Close)
                {
                    continue;
                }

                if (worldBox.BoxState == WorldBox.State.Empty)
                {
                    worldBox.BoxState = WorldBox.State.Interactive;
                }

                if (_selectedMember.WorldPosition == worldBox.WorldPosition)
                {
                    Debug.LogError("selected");
                    worldBox.BoxState = WorldBox.State.Selected;
                }
            }

            Messenger<List<WorldBox>>.Broadcast(Constnts.Events.GameField.UpdateBoxes, listOfSelectedBoxes);
        }
    }
}