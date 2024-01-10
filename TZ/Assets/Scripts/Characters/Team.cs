using System.Collections.Generic;
using Characters;

public class Team
{
    public enum RelationshipType
    {
        Enemy,
        Player,
    }

    public RelationshipType Relationship;
    public List<MemberData> MembersData;
}