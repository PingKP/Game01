using UnityEngine;
using System.Collections.Generic;

public class HealthComponent : Component
{
    public int currentHealth;
    public int maxHealth;
    public bool isAlive;
}


public class HealthSystem
{
    public bool TakeDamage(HealthComponent healthComponent, int damage)
    {
        healthComponent.currentHealth -= damage;
        if (healthComponent.currentHealth <= 0)
        {
            healthComponent.isAlive = false;
            healthComponent.currentHealth = 0;
        }
        return healthComponent.isAlive;
    }

    public bool Heal(HealthComponent healthComponent, int healAmount)
    {
        if (!healthComponent.isAlive)
            return false;
        healthComponent.currentHealth += healAmount;
        if (healthComponent.currentHealth > healthComponent.maxHealth)
        {
            healthComponent.currentHealth = healthComponent.maxHealth;
        }
        return true;
    }
}


public class HealthManager : ComponentManager<HealthComponent>
{
    private HealthSystem healthSystem;

    public void Initialize(HealthSystem system)
    {
        healthSystem = system;
    }

    public void CreateHealthComponent(uint entityId, int maxHealth)
    {
        HealthComponent newHealthComponent = new HealthComponent
        {
            ownerId = entityId,
            maxHealth = maxHealth,
            currentHealth = maxHealth,
            isAlive = true
        };
        AddComponent(entityId, newHealthComponent);
    }
}