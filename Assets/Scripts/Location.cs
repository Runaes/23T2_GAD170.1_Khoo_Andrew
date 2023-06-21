using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Location : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Dictionary<Direction, string> validExits;
    Func<PlayerCharacter, Enemy> onEnterAction;

    public Location(Dictionary<Direction,string> validExits, Func<PlayerCharacter, Enemy> onEnterAction)
    {
        this.validExits = validExits;
        this.onEnterAction = onEnterAction;
    }

    public void EnterLocation(PlayerCharacter player)
    {
        LocationManager.currentLocation = this;
        var newEnemy = onEnterAction?.Invoke(player);
        if (enemy == null)
        {
            enemy = newEnemy;
        }
        else
        {
            TextManager.NewLine($"The foe is still here! it has {enemy.health} hp left!");
        }
    }

    public void KillEnemy()
    {
        TextManager.NewLine($"Woo! You killed it! and gained {enemy.expValue} exp!");
        enemy = null;
    }

    public void Fight(PlayerCharacter player)
    {
        if (enemy == null)
        {
            TextManager.NewLine("SCHWING AND A MISS!");
            return;
        }
        enemy.TakeDamage(player);
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
            if (!validExits.TryGetValue(direction, out var location))
            {
                player.BumpIntoWall(false);
                if (player.bumpCount < 2)
                {
                    TextManager.NewLine("You bump into a wall!");
                }
                else if (player.bumpCount < 6)
                {
                    TextManager.NewLine("The wall has not budged, but a few of your brain cells have fallen out.");
                }
                else if (player.bumpCount < 20)
                {
                    TextManager.NewLine("And there goes another braincell......");
                }
                else
                {
                    TextManager.NewLine("The brain cells start combining together moving towards the fountain!");
                    player.goldenSlime = true;
                }
            }
            else
            {
                player.BumpIntoWall(true);
            }
            return location;
        }
        return null;
    }

    bool AttemptToEscape(PlayerCharacter player)
    {
        return enemy.AttemptEscape(player);
    }
}
