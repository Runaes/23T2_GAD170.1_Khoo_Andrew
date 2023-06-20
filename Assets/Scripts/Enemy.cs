using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int expValue;
    [SerializeField] private int damage;
    Action kill;

    public Enemy(int health, int expValue, int damage, Action kill)
    {
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

    public bool AttemptEscape(PlayerCharacter player)
    {
        return false;
    }
}