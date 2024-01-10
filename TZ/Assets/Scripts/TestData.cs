using System.Collections.Generic;
using System.Linq;
using Characters;
using DefaultNamespace;
using UnityEngine;

public class TestData
{
    public List<MemberData> AvailableMembers;
    public List<Member> CharactersDatabase;
    public List<Location> LocationsDatabase;

    public void InitCharactersDatabase()
    {
        CharactersDatabase = new List<Member>()
        {
            new Characters.Member()
            {
                AddressableKey = "Pig_char",
                Stats = new Dictionary<string, Stat>()
                {
                    { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                    { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } },
                    { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } }
                },
                Abilities = new List<Ability>()
                {
                    new Ability()
                    {
                        ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                            { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                        }
                    },
                    new Ability()
                    {
                        ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                        }
                    },
                }
            },
            new Characters.Member()
            {
                AddressableKey = "Penguin_char",
                Stats = new Dictionary<string, Stat>()
                {
                    { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                    { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } },
                    { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } }
                },
                Abilities = new List<Ability>()
                {
                    new Ability()
                    {
                        ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                            { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                        }
                    },
                    new Ability()
                    {
                        ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                        }
                    },
                }
            },
            new Characters.Member()
            {
                AddressableKey = "Dog_char",
                Stats = new Dictionary<string, Stat>()
                {
                    { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                    { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } },
                    { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } }
                },
                Abilities = new List<Ability>()
                {
                    new Ability()
                    {
                        ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                            { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                        }
                    },
                    new Ability()
                    {
                        ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                        {
                            { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                            { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                        }
                    },
                }
            }
        };
    }

    public Member GetCharacter(string id)
    {
        var character = CharactersDatabase.FirstOrDefault(x => x.AddressableKey.Equals(id));
        if (character == null)
        {
            Debug.LogError($"member vith id: {id}   not exist");
        }

        return character;
    }

    public void InitAvailableMembersDatabase()
    {
        AvailableMembers = new List<MemberData>()
        {
            new MemberData()
            {
                Member = new Characters.Member()
                {
                    AddressableKey = "Pig_char",
                    Stats = new Dictionary<string, Stat>()
                    {
                        { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                        { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } },
                        { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } }
                    },
                    Abilities = new List<Ability>()
                    {
                        new Ability()
                        {
                            ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                                { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                            }
                        },
                        new Ability()
                        {
                            ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                            }
                        },
                    }
                }
            },
            new MemberData()
            {
                Member = new Characters.Member()
                {
                    AddressableKey = "Penguin_char",
                    Stats = new Dictionary<string, Stat>()
                    {
                        { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                        { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } },
                        { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } }
                    },
                    Abilities = new List<Ability>()
                    {
                        new Ability()
                        {
                            ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                                { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                            }
                        },
                        new Ability()
                        {
                            ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                            }
                        },
                    }
                }
            },
            new MemberData()
            {
                Member = new Characters.Member()
                {
                    AddressableKey = "Dog_char",
                    Stats = new Dictionary<string, Stat>()
                    {
                        { Constnts.Stats.HealthPoints, new Stat() { ID = Constnts.Stats.HealthPoints, Value = 10 } },
                        { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 10 } },
                        { Constnts.Stats.MaxActionPoints, new Stat() { ID = Constnts.Stats.MaxActionPoints, Value = 10 } }
                    },
                    Abilities = new List<Ability>()
                    {
                        new Ability()
                        {
                            ID = Constnts.Ability.Attack, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 6 } },
                                { Constnts.Stats.Damage, new Stat() { ID = Constnts.Stats.Damage, Value = 3 } },
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 1 } },
                            }
                        },
                        new Ability()
                        {
                            ID = Constnts.Ability.Move, Stats = new Dictionary<string, Stat>()
                            {
                                { Constnts.Stats.Distance, new Stat() { ID = Constnts.Stats.Distance, Value = 4 } },
                                { Constnts.Stats.ActionPoints, new Stat() { ID = Constnts.Stats.ActionPoints, Value = 4 } },
                            }
                        },
                    }
                }
            }
        };
    }

    public void InitLocationDatabase()
    {
        LocationsDatabase = new List<Location>()
        {
            new Location()
            {
                ID = "L1",
                XSize = 10,
                YSize = 10,
                PlayerMaxMemberCount = 1,
                PlayerTeamPlaces = new List<WorldPosition>()
                {
                    new WorldPosition() { X = 0, Y = 5 },
                },

                Teams = new List<Team>()
                {
                    new Team()
                    {
                        Relationship = Team.RelationshipType.Enemy,
                        MembersData = new List<MemberData>()
                        {
                            new MemberData() { Member = GetCharacter("Penguin_char"), WorldPosition = new WorldPosition() { X = 9, Y = 4 } },
                        }
                    }
                },
                Environment = new List<EnvironmentObject>()
                {
                    new EnvironmentObject() { ID = "Tree_props", WorldPosition = new WorldPosition() { X = 1, Y = 1 } },
                    new EnvironmentObject() { ID = "Tree_props", WorldPosition = new WorldPosition() { X = 1, Y = 2 } },
                    new EnvironmentObject() { ID = "Rock_props", WorldPosition = new WorldPosition() { X = 1, Y = 3 } },
                }
            },
            new Location()
            {
                ID = "L2",
                XSize = 15,
                YSize = 10,
                PlayerMaxMemberCount = 3,
                PlayerTeamPlaces = new List<WorldPosition>()
                {
                    new WorldPosition() { X = 0, Y = 3 },
                    new WorldPosition() { X = 0, Y = 4 },
                    new WorldPosition() { X = 0, Y = 5 },
                },
                Teams = new List<Team>()
                {
                    new Team()
                    {
                        Relationship = Team.RelationshipType.Enemy,
                        MembersData = new List<MemberData>()
                        {
                            new MemberData() { Member = GetCharacter("Pig_char"), WorldPosition = new WorldPosition() { X = 14, Y = 3 } },
                            new MemberData() { Member = GetCharacter("Penguin_char"), WorldPosition = new WorldPosition() { X = 14, Y = 4 } },
                            new MemberData() { Member = GetCharacter("Dog_char"), WorldPosition = new WorldPosition() { X = 14, Y = 5 } },
                        }
                    }
                },
                Environment = new List<EnvironmentObject>()
                {
                    new EnvironmentObject() { ID = "Tree_props", WorldPosition = new WorldPosition() { X = 1, Y = 1 } },
                    new EnvironmentObject() { ID = "Rock_props", WorldPosition = new WorldPosition() { X = 1, Y = 2 } },
                    new EnvironmentObject() { ID = "Tree_props", WorldPosition = new WorldPosition() { X = 1, Y = 3 } },
                }
            }
        };
    }
}