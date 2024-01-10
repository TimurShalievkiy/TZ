using System.Collections.Generic;

public class Location
{
    public string ID;
    public int XSize;
    public int YSize;
    public int PlayerMaxMemberCount;
    public List<WorldPosition> PlayerTeamPlaces;
    public List<Team> Teams;
    public List<EnvironmentObject> Environment;
}