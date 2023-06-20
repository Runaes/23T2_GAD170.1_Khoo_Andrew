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

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            var key = Direction.None;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                key = Direction.Forward;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                key = Direction.Back;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                key = Direction.Left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                key = Direction.Right;
            }

            if (key != Direction.None)
            {
                return;
            }
        }
    }

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

    public void SetHealthToFull()
    {
        health = level * 150 - 50;
    }

    public bool ApplyHiddenCheck(bool hidden)
    {
        if (hidden)
        {
            bumpCount++;
        }
        else
        {  
            bumpCount = 0;
        }
        return bumpCount > 9;
    }
    int bumpCount = 0;
}
