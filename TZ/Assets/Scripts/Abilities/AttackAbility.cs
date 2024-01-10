using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class AttackAbility
    {
        private MemberData _selectedMember;
        private Ability _ability;
        private List<WorldBox> _worldBoxes;
        private List<Team> _teams;
        private bool _isActive;
        private MemberData _targetMember;
        public AttackAbility()
        {
            Messenger<List<WorldBox>>.AddListener(Constnts.Events.GameField.NewLevelCreated, OnLevelCreated);
            Messenger<MemberData>.AddListener(Constnts.Events.Game.HUDMemberClick, SelectMember);

            Messenger<WorldBox>.AddListener(Constnts.Events.Game.BoxClick, OnBoxClick);
            Messenger<List<Team>>.AddListener(Constnts.Events.GameField.CreateTeams, OnTeamsCreated);
        }

        private void OnTeamsCreated(List<Team> teams)
        {
            _teams = teams;
        }

        private void OnBoxClick(WorldBox worldBox)
        {
            if (!_isActive)
            {
                return;
            }
            
            if (worldBox.BoxState != WorldBox.State.Enemy)
            {
                return;
            }

            SpendPoints(worldBox);
            DoDamage(worldBox);
            Messenger<MemberData>.Broadcast(Constnts.Events.Game.MemberUpdate, _targetMember);
            Messenger<Ability>.Broadcast(Constnts.Events.Game.ActionClick, new Ability() { ID = String.Empty });
        }

        private void SpendPoints(WorldBox worldBox)
        {
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
        }

        private void DoDamage(WorldBox worldBox)
        {
            _targetMember = null;
            foreach (var team in _teams)
            {
                _targetMember = team.MembersData.FirstOrDefault(x => x.WorldPosition == worldBox.WorldPosition);
                if (_targetMember != null)
                {
                    break;
                }
            }

            if (_targetMember == null)
            {
                return;
            }

            var memberDamage = _ability.Stats[Constnts.Stats.Damage];
            var targetHp = _targetMember.Member.Stats[Constnts.Stats.HealthPoints];
            targetHp.Value -= memberDamage.Value;
            _targetMember.Member.Stats[Constnts.Stats.HealthPoints] = targetHp;
            Debug.LogError($"{_targetMember.Member.AddressableKey} id {_targetMember.Member.ID} {targetHp.ID} = {targetHp.Value}");
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
            if (!_ability.Stats.TryGetValue(Constnts.Stats.Distance, out var damageDistance))
            {
                return;
            }

            int minI = _selectedMember.WorldPosition.X - damageDistance.Value;
            int maxI = _selectedMember.WorldPosition.X + damageDistance.Value;

            int minJ = _selectedMember.WorldPosition.Y - damageDistance.Value;
            int maxJ = _selectedMember.WorldPosition.Y + damageDistance.Value;

            var listOfSelectedBoxes = _worldBoxes.Where(x => x.WorldPosition.X >= minI && x.WorldPosition.X <= maxI &&
                                                             x.WorldPosition.Y >= minJ && x.WorldPosition.Y <= maxJ).ToList();
            List<WorldBox> result = new List<WorldBox>();
            foreach (var team in _teams)
            {
                foreach (var member in team.MembersData)
                {
                    var box = listOfSelectedBoxes.FirstOrDefault(x => x.WorldPosition == member.WorldPosition);
                    if (box != null)
                    {
                        result.Add(box);
                    }
                }
            }

            foreach (var worldBox in result)
            {
                if (worldBox.BoxState == WorldBox.State.Close)
                {
                    continue;
                }

                if (worldBox.BoxState == WorldBox.State.Empty)
                {
                    worldBox.BoxState = WorldBox.State.Enemy;
                }

                if (_selectedMember.WorldPosition == worldBox.WorldPosition)
                {
                    worldBox.BoxState = WorldBox.State.Selected;
                }
            }

            Messenger<List<WorldBox>>.Broadcast(Constnts.Events.GameField.UpdateBoxes, result);
        }
    }
}