using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public enum ControlType { Player, AI }
    [SerializeField] private ControlType controlType = ControlType.Player;
    public ControlType GetControlType { get { return controlType; } }

    public enum PlayerType { First, Second }
    [SerializeField] private PlayerType playerType = PlayerType.First;
    public PlayerType GetPlayerType { get { return playerType; } }

    private string username = "";
    public string Username { get { return username; } }

    private float currentHealth = 100.0f;
    public float CurrentHealth { get { return currentHealth; } }
    private float maxHealth = 100.0f;

    public void Setup(string username, ControlType controlType, PlayerType playerType)
    {
        this.username = username;
        this.controlType = controlType;
        this.playerType = playerType;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Dead", this);
        // TODO: Do something
    }
}
