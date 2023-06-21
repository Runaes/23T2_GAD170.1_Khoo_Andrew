using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int health;
    [SerializeField] private double attack;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        exp = 0;
        health = 100;
        attack = 10;
        goldenSlime = false;
        TextManager.NewLine("Welcome to the Game! Please use arrow keys to move and the spacebar to attack! Or restart the Game using the F5 key\r\n\r\n");
        LocationManager.MoveDirectlyToLocation("Starting Fountain", this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Start();
            }
            if (level < 5)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    LevelUp();
                }
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

                if (key == Direction.None)
                {
                    return;
                }
                TextManager.ResetLines();
                LocationManager.MoveToNewLocation(key, this);
            }
        }
        if (level == 5)
        {
            TextManager.NewLine("WooHoo You win!");
            level++;
        }
    }

    public bool TakeDamage(int damageTaken)
    {
        health -= damageTaken;

        TextManager.NewLine($"OOFT. You were hit for {damageTaken}. You now have {health} life left.");
        return AdjustStats();
    }

    public int DoDamage()
    {
        TextManager.NewLine($"SCHWING! You do {(int)attack} damage.");
        return (int)attack;
    }

    public void GainExp(int expGained)
    {
        exp += expGained;
        AdjustStats();
    }

    public bool AdjustStats()
    {
        if (CanLevelUp)
        {
            TextManager.NewLine("You have enough Exp to level up. Press L to LevelUp!");
        }
        if (health <= 0)
        {
            var oldexpValue = exp;
            var newexpValue = Math.Max(0, exp -= (level * 25));
            exp = Math.Max(0, exp);
            TextManager.NewLine($"You pass out and find yourself back at the starting fountain, {(newexpValue == oldexpValue ? "but you have no exp left to Lose! You sly dog!" : $"and you lost {oldexpValue - newexpValue} exp!")}");
            LocationManager.MoveDirectlyToLocation("Starting Fountain", this);
            return true;
        }

        return false;
    }

    private bool CanLevelUp
    {
        get
        {
            return exp > ExpRequiredToLevelUp && level < 5;

        }
    }

    private int ExpRequiredToLevelUp
    {
        get
        {
            return (level * 75) + 25;
        }
    }

    private void LevelUp()
    {
        if (CanLevelUp)
        {
            exp -= ExpRequiredToLevelUp;
            level++;
            SetHealthToFull();
            attack *= 1.7525;
            TextManager.NewLine($"Woo Hoo! level UP! You are now level {level}");
            AdjustStats();
        }
        else
        {
            TextManager.NewLine($"You try to level up but just aren't experienced enough. You need {ExpRequiredToLevelUp - exp} more exp.");
        }
    }

    public void SetHealthToFull()
    {
        health = level * 150 - 50;
    }

    public void BumpIntoWall(bool reset)
    {
        if (!reset)
        {
            bumpCount++;
        }
        else
        {  
            bumpCount = 0;
        }
        if (bumpCount > 19)
        {
            goldenSlime = true;
        }
    }

    public int bumpCount { get; private set; } = 0;
    public bool goldenSlime { get; set; }
}
