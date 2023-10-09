using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private UnitStatsSO unitStats;

    private float maxHealth;
    private float currentHealth;
    public event EventHandler OnUnitDied;


    void Awake() {
        maxHealth = unitStats.MaxHealth;
        HealthBar.maxValue = maxHealth;
        SetMaxHealth();
        DisplayCurrentHealth();
    }

    private void SetMaxHealth() {
        currentHealth = maxHealth;
    }

    private void DisplayCurrentHealth() {
        HealthBar.value = currentHealth;
    }

    public void TakeDamage(float damage) {
        if(damage > 0) {
            if(damage >= currentHealth) {
                Death();
            } else {
                currentHealth -= damage;
                DisplayCurrentHealth();
            }
        }
    }

    protected virtual void Death() {
        OnUnitDied?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void SetCurrentHealth(float health) {
        if(health > 0) {
            currentHealth = health;
            DisplayCurrentHealth();
        }
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }
}
