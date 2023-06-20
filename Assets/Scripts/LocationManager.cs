using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    Dictionary<string, Location> locations;

    public LocationManager()
    {
    }

    public void MoveToNewLocation(Direction direction, Location currentLocation, PlayerCharacter player)
    {
        if (currentLocation?.GetNewLocation(direction, player) is string newLocation)
        {
            locations[newLocation].EnterLocation();
        }
    }

    public void MoveDirectlyToLocation(string location)
    {
        locations[location].EnterLocation();
    }

    void CreateLocations()
    {
        locations.Add(
            "Starting Fountain", 
            new Location(
                new Dictionary<Direction,string> {(Direction.Forward, "First Room")},
                new Func<Enemy, PlayerCharacter>(player => 
                            {
                                player.ApplyHiddenCheck(false);
                                player.SetHealthToFull();
                                Debug.Log($"You drink from the fountain and heal to full.");
                                return null;
                            })
                        )
                    );

        locations.Add(
            "First Room",
            new Location(
                new Dictionary<Direction,string> { (Direction.Back, "Starting Fountain"), (Direction.Left, "Esey Room"), (Direction.Forward, "Second Room")},
                new Func<Enemy, PlayerCharacter>(player =>
                            {
                                if(player.ApplyHiddenCheck(true))
                                {
                                    return new Enemy(1,5000,9001);
                                    Debug.Log("Hey look, a golden slime has formed from the accumulation of your lost brain cells!")
                                }
                            })
                        )
                    );
    }
}
