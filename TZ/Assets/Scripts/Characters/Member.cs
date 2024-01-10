using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine.Serialization;

namespace Characters
{
    [Serializable]
    public class Member
    {
        public string ID;
        public string AddressableKey;
        public WorldPosition WorldPosition;
        public Dictionary<string, Stat> Stats;
        public List<Spell> Spells;
        public List<Ability> Abilities;

        public Member()
        {
        }

        public Member(Member member)
        {
            AddressableKey = member.AddressableKey;
            ID = member.ID;
            WorldPosition = member.WorldPosition;
            Stats = member.Stats;
            Spells = member.Spells;
            Abilities = member.Abilities;
        }
    }
}