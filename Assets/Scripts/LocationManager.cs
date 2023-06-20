using System;
using System.Collections.Generic;
using UnityEngine;

public static class LocationManager
{
    static Dictionary<string, Location> locations;
    public static Location currentLocation;
    public static void MoveToNewLocation(Direction direction, PlayerCharacter player)
    {
        if (currentLocation?.GetNewLocation(direction, player) is string newLocation)
        {
            locations[newLocation].EnterLocation(player);
        }
    }

    public static void Fight(PlayerCharacter player)
    {

    }

    public static void MoveDirectlyToLocation(string location, PlayerCharacter player)
    {
        if (locations == null)
        {
            CreateLocations();
        }

        locations[location].EnterLocation(player);
    }

    public static void Kill(string location)
    {
        locations[location].KillEnemy();
    }

    static void CreateLocations()
    {
        var startingLocation = new Location(
                new Dictionary<Direction, string> { { Direction.Forward, "First Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    TextManager.NewLine("You find yourself in the Starting Fountin. There is an obvious exit in front of you.");
                    player.SetHealthToFull();
                    TextManager.NewLine("You drink from the fountain and heal to full.");
                    return null;
                })
                        );


        locations = new Dictionary<string, Location>
        {
            {
                "Starting Fountain",
                startingLocation
            },

            {
                "First Room",
                new Location(
                new Dictionary<Direction, string> { { Direction.Back, "Starting Fountain" }, { Direction.Left, "Esey Room" }, { Direction.Forward, "Second Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                            {
                                TextManager.NewLine(
@"You find yourself in the first room. There is an exit behind you leading back to the Starting Fountain,
In front of you Leading to the Second Room,
and to your left, leading into what seems to be an Easy Room.");
                                if (player.BumpCount > 10 || player.GoldenSlime)
                                {
                                    player.GoldenSlime = false;
                                    TextManager.NewLine("Hey look, a golden slime has formed from the accumulation of your lost brain cells!");
                                    return new Enemy(health: 1, expValue: 100, damage: 9001, kill: () => Kill("First Room"));
                                }
                                return null;
                            })
                        )
            },


            {
                "Esey Room",
                new Location(
                new Dictionary<Direction, string> { { Direction.Right, "First Room" }, { Direction.Left, "Esey Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    TextManager.NewLine("You find yourself in the Easy room. There is an exit to your right going back to the first room and a portal to your left that seems to loop back here.");
                    return new Enemy(health: 15, expValue: 10, damage: 5, kill: () => Kill("Esey Room"));
                })
                        )
            },

            {
                "Second Room",
                new Location(
                new Dictionary<Direction, string> { { Direction.Right, "Hard Room" }, { Direction.Left, "Medium Room" }, { Direction.Forward, "Hell" }, { Direction.Back, "First Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    TextManager.NewLine(
@"You find yourself in the Second room. 
There is an exit to behind you going back to the first room.
There is an exit to your left heading to the Medium room.
There is an exit to your right heading to the Hard room.
And a portal in front that seems to lead straight into hell.");
                    return null;
                })
                    )
            },

            {
                "Medium Room",
                new Location(
                new Dictionary<Direction, string> { { Direction.Right, "Second Room" }, { Direction.Left, "Medium Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    TextManager.NewLine("You find yourself in the Medium room. There is an exit to your right going back to the second room and a portal to your left that seems to loop back here.");
                    return new Enemy(health: 80, expValue: 50, damage: 40, kill: () => Kill("Medium Room"));
                })
                    )
            },

            {
                "Hard Room",
                new Location(
                new Dictionary<Direction, string> { { Direction.Right, "Hard Room" }, { Direction.Left, "Second Room" } },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    TextManager.NewLine("You find yourself in the Hard room. There is an exit to your left going back to the second room and a portal to your right that seems to loop back here.");
                    return new Enemy(health: 200, expValue: 100, damage: 100, kill: () => Kill("Hard Room"));

                }))
            },

            {
                "Hell",
                new Location(
                new Dictionary<Direction, string> { },
                new Func<PlayerCharacter, Enemy>(player =>
                {
                    if (player.GoldenSlime)
                    {
                        TextManager.NewLine(
@"You've found your way into Hell, Face to face with the final boss.
Fortunately you've lost so many brain cells that you don't even notice that you should be burning alive here!");
                        return new Enemy(health: 1, expValue: 5000000, damage: 0, kill: null); // there is no need to clear the enemy. Player will win if they defeat a boss
                    }

                    TextManager.NewLine("You fall though the portal deep into hell. It's hot here and the heat burns your skin!");
                    if (!player.TakeDamage(399))
                    {
                        TextManager.NewLine("You grimace from the burning heat and see before you the final boss!");
                        return new Enemy(health: 100, expValue: 5000000, damage: 50, kill: null); // there is no need to clear the enemy. Player will win if they defeat a boss
                    }
                    return null;
                })
                        )
            }
        };
    }
}
