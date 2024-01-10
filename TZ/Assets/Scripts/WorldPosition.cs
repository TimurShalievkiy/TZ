using System;

[Serializable]
public struct WorldPosition
{
    public int X;
    public int Y;

    public static bool operator ==(WorldPosition p1, WorldPosition p2)
    {
        return ((p1.X == p2.X) && (p1.Y == p2.Y));
    }

    public static bool operator !=(WorldPosition p1, WorldPosition p2)
    {
        return !(p1 == p2);
    }
}