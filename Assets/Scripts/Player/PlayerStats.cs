using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    public float coins; //{ get; private set; }

    [Header("Player Parameters")]
    //economy
    public float defaultPlayerDamage;
    public float defaultPlayerMovementSpeed;
    public float defaultPlayerJump;

    private float currentPlayerDamage;
    private float currentPlayerMovementSpeed;
    private float currentPlayerJump;

    private PlayerController controller;
    private PlayerCombat combat;

    private void Awake()
    {
        //[] Apply the player parameters to the player controller
        currentPlayerDamage = defaultPlayerDamage;
        currentPlayerMovementSpeed = defaultPlayerMovementSpeed;
        currentPlayerJump = defaultPlayerJump * 100f;

        controller = gameObject.GetComponent<PlayerController>();
        combat = gameObject.GetComponent<PlayerCombat>();
        if(combat != null && controller != null)
        {
            controller.playerSpeed = currentPlayerMovementSpeed;
            controller.jumpPower = currentPlayerJump;
            combat.damage = currentPlayerDamage;
        }
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false); // Makes the player invisible but still in the scene

        // Finds and sets the GameManager Script instance from the scene
        GameManager_Temporary manager = GameManager_Temporary.instance.GetComponent<GameManager_Temporary>();
        if(manager != null)
        {
            manager.restartLevel();
        }
        else if (manager == null)
        {
            GameManager_Temporary.instance.restartLevel();
            Debug.LogError("Catastrophic Error, pulling and restarting!");
        }
    }

    public void ModifyPlayerEquipmentStats(float damage, float movementSpeed, float armor)
    {
        currentPlayerDamage += damage;
        currentPlayerMovementSpeed += movementSpeed;
        Armor += armor;

        controller.playerSpeed = currentPlayerMovementSpeed;
        combat.damage = currentPlayerDamage;
    }
    public void TemporaryModifyPlayerItemStats(float movementSpeed, float jump, float time)
    {
        if(jump != 0f)
        {
            currentPlayerJump = currentPlayerJump + jump * 100f;
        }
        currentPlayerMovementSpeed += movementSpeed;
        controller.playerSpeed = currentPlayerMovementSpeed;
        controller.jumpPower = currentPlayerJump;
        Debug.Log("Added temporary item stats");
        StartCoroutine(returnToDefault(time, movementSpeed, jump));
    }

    IEnumerator returnToDefault(float time, float movementSpeed, float jump)
    {
        yield return new WaitForSeconds(time);
        currentPlayerMovementSpeed -= movementSpeed;
        currentPlayerJump -= jump * 100f;
        controller.playerSpeed = currentPlayerMovementSpeed;
        controller.jumpPower = currentPlayerJump;
        Debug.Log("Reseted stats");
        StopAllCoroutines();
    }

    public void CurrencyAdd(float amount)
    {
        // procedure that adds coins to the player
        coins = coins + amount;
        Debug.Log("Added " + coins + " coins to the player's wallet!");
    }

    public void CurrencySubtract(float amount)
    {
        coins = coins - amount;
        Debug.Log("Deducted " + amount + " coins from the user's wallet!");
    }
}
