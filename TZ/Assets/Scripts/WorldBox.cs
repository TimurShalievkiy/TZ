using System;

[Serializable]
public class WorldBox
{
    public enum State
    {
        None,
        Empty,
        Enemy,
        Selected,
        Close,
        Interactive
    }

    public WorldPosition WorldPosition;
    public string ItemID;
    public State BoxState;
}