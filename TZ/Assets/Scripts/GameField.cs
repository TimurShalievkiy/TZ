using System;
using System.Collections.Generic;
using System.Linq;
using Core.MessengerStatic;
using DefaultNamespace;

public class GameField
{
    private List<WorldBox> _worldBoxes;

    public void Setup(Location location)
    {
        _worldBoxes = new List<WorldBox>();
        CreateBoxes(location);
        CreateProps(location);

        Messenger<List<WorldBox>>.Broadcast(Constnts.Events.GameField.NewLevelCreated, _worldBoxes);
    }

    private void CreateProps(Location location)
    {
        foreach (var environmentObject in location.Environment)
        {
            var obj = _worldBoxes.FirstOrDefault(x => x.WorldPosition == environmentObject.WorldPosition);
            if (obj == null)
            {
                continue;
            }

            obj.ItemID = environmentObject.ID;
            obj.BoxState = WorldBox.State.Close;
        }
    }

    private void CreateBoxes(Location location)
    {
        for (int i = 0; i < location.XSize; i++)
        {
            for (int j = 0; j < location.YSize; j++)
            {
                var newBox = new WorldBox()
                {
                    WorldPosition = new WorldPosition() { X = i, Y = j },
                    ItemID = String.Empty, 
                    BoxState = WorldBox.State.Empty
                };

                _worldBoxes.Add(newBox);
            }
        }
    }

    public void ClearGameField()
    {
        _worldBoxes?.Clear();
    }
}