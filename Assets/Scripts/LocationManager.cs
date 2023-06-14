using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    Dictionary<string, Location> locations;

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
}
