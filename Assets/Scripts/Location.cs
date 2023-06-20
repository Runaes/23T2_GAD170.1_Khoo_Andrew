using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Location : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Dictionary<Direction, string> validExits;
    Func<Enemy> onEnterAction;

    public Location(Dictionary<Direction,string> validExits, Func<Enemy, PlayerCharacter> onEnterAction)
    {
        this.validExits = validExits;
        this.onEnterAction = onEnterAction;
    }

    public void EnterLocation(PlayerCharacter player)
    {
        if (enemy == null)
        {
            enemy = onEnterAction?.Invoke(player);
        }
    }

    public string GetNewLocation(Direction direction, PlayerCharacter player)
    {
        var canMove = true;
        if (enemy != null)
        {
            canMove = AttemptToEscape(player);
        }
        if (canMove)
        {
            validExits.TryGetValue(direction, out var location);
            return location;
        }
        return null;
    }

    bool AttemptToEscape(PlayerCharacter player)
    {
        return enemy.AttemptEscape(player);
    }
}
