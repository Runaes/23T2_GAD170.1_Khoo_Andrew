using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int exp = 0;
    [SerializeField] private int health = 100;
    [SerializeField] private double attack = 10;
    [SerializeField] private string currentLocation = "Starting Fountain";

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        AdjustStats();
    }

    public int DoDamage()
    {
        return (int)attack;
    }

    public void GainExp(int expGained)
    {
        exp += expGained;
        AdjustStats();
    }

    public void AdjustStats()
    {
        var expRequiredForlevelUp =  (level * 75) + 25;
        if (exp == expRequiredForlevelUp)
        {
            exp -= expRequiredForlevelUp;
            level++;
            SetHealthToFull();
            attack *= 1.2525;
            Debug.Log($"Woo Hoo! level UP! You are now level {level}");
        }

        if (health <= 0)
        {
            var newexpValue = Math.Max(0, exp -= (level * 25));
            var oldexpValue = exp;
            Debug.Log($"You pass out and find yourself back at the starting fountain, {(newexpValue == oldexpValue ? "but you have no exp left to Lose! You sly dog!" : $"and you lost {oldexpValue - newexpValue} exp!")}");
            currentLocation = "Starting Fountain";
        }
    }

    private void SetHealthToFull()
    {
        health = level * 150 - 50;
    }
}
