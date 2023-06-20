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

    // Start is called before the first frame update
    void Start()
    {        
        LocationManager.MoveDirectlyToLocation("Starting Fountain", this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LocationManager.Fight(this);
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
            LocationManager.MoveToNewLocation(key, this);
        }
    }

    public bool TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        return AdjustStats();
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

    public bool AdjustStats()
    {
        var expRequiredForlevelUp =  (level * 75) + 25;
        if (exp == expRequiredForlevelUp)
        {
            exp -= expRequiredForlevelUp;
            level++;
            SetHealthToFull();
            attack *= 1.2525;
            TextManager.NewLine($"Woo Hoo! level UP! You are now level {level}");
        }

        if (health <= 0)
        {
            var newexpValue = Math.Max(0, exp -= (level * 25));
            var oldexpValue = exp;
            TextManager.NewLine($"You pass out and find yourself back at the starting fountain, {(newexpValue == oldexpValue ? "but you have no exp left to Lose! You sly dog!" : $"and you lost {oldexpValue - newexpValue} exp!")}");
            LocationManager.MoveDirectlyToLocation("Starting Fountain", this);
            return true;
        }

        return false;
    }

    public void SetHealthToFull()
    {
        health = level * 150 - 50;
    }

    public void BumpIntoWall(bool reset)
    {
        if (!reset)
        {
            BumpCount++;
        }
        else
        {  
            BumpCount = 0;
        }
        if (BumpCount > 19)
        {
            GoldenSlime = true;
        }
    }

    public int BumpCount { get; private set; } = 0;
    public bool GoldenSlime { get; set; }
}
