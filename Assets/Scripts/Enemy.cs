using System;
using UnityEngine;

public class Enemy
{
    [SerializeField] public int health;
    [SerializeField] public int expValue;
    [SerializeField] private int damage;
    Action kill;

    public Enemy(string name, int health, int expValue, int damage, Action kill)
    {
        TextManager.NewLine($"A {name} appears! It looks like it does {damage} damage and is worth about {expValue}!");
        this.health = health;
        this.expValue = expValue;
        this.damage = damage;
        this.kill = kill;
    }

    public void TakeDamage(PlayerCharacter player)
    {
        health -= player.DoDamage();
        if (health <= 0)
        {
            player.GainExp(expValue);
            kill?.Invoke();
        }
        else   
        {
            DoDamage(player);
        }
    }

    public void DoDamage(PlayerCharacter player)
    {
        player.TakeDamage(damage);
    }

    Unity.Mathematics.Random rand = new Unity.Mathematics.Random();
    bool first = true;
    public bool AttemptEscape(PlayerCharacter player)
    {
        if (first)
        {
            first = false;
            rand.InitState();
        }

        var success = rand.NextInt(100) > 50;
        if (!success)
        {
            TextManager.NewLine("You failed to escape!");
            DoDamage(player);
        }
        else
        {
            TextManager.NewLine("You manage to run past your foe!");
        }
        return success;
    }
}