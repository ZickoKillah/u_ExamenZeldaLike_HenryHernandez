using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Attack), typeof(Interact))]
public class PlayerController : TopDownCharacterController, IDamageable, IHealable
{
    [Header("Salud del Jugador")]
    [SerializeField] private int maxHearts = 3;
    [SerializeField] private int healthPerHeart = 4;
    
    private int currentHealth;
    [Header("Almas")] 
    [SerializeField] private int soulCount = 0;
    
    
    [Header("Inventario")]
    private List<ItemDataSO> inventory = new List<ItemDataSO>();
    public int KeyCount => inventory.FindAll(item => item.itemType == ItemType.Key).Count;
    public bool HasSwordUpgrade => inventory.Exists(item => item.itemType == ItemType.SwordUpgrade);
    public int PotionCount => inventory.FindAll(item => item.itemType == ItemType.Potion).Count;

    private Attack attackComponent;
    private Interact interactComponent;

    private void Start()
    {
        attackComponent = GetComponent<Attack>();
        interactComponent = GetComponent<Interact>();
        currentHealth = maxHearts * healthPerHeart;
    }

    public void TakeDamage(float damage)
    {
        int damageInt = Mathf.RoundToInt(damage);
        currentHealth -= damageInt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHearts * healthPerHeart);
        UIManager.Instance.UpdateHearts(currentHealth, maxHearts, healthPerHeart);
        Debug.Log($"¡Jugador dañado! Salud actual: {currentHealth}/{maxHearts * healthPerHeart}");
        if (attackComponent != null)
        {
            attackComponent.EndAttack();
        }
        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        int healInt = Mathf.RoundToInt(amount);
        currentHealth += healInt;
        currentHealth = Mathf.Min(currentHealth, maxHearts * healthPerHeart);
        UIManager.Instance.UpdateHearts(currentHealth, maxHearts, healthPerHeart);
        Debug.Log($"¡Jugador curado! Salud actual: {currentHealth}/{maxHearts * healthPerHeart}");
    }

    private void Die()
    {
        Debug.Log("El héroe ha caído...");
        EventBus.InvokeOnPlayerDeath();
        gameObject.SetActive(false);
    }

    public void AddItem(ItemDataSO item)
    {
        inventory.Add(item);
        Debug.Log($"Item recogido: {item.itemName}");
        EventBus.InvokeOnItemCollected(item);

        if (item.itemType == ItemType.Key)
            UIManager.Instance.UpdateKeyCount(KeyCount);
        else if (item.itemType == ItemType.Potion)
            UIManager.Instance.UpdatePotionCount(PotionCount);
        else if (item.itemType == ItemType.SwordUpgrade)
        {
            // Aumentar el daño del ataque
            Attack playerAttack = GetComponent<Attack>();
            if (playerAttack != null)
                playerAttack.damage += item.effectValue;
            // Mostrar icono en el HUD
            UIManager.Instance.ShowSwordUpgradeIcon();
        }
        else if (item.itemType == ItemType.Soul)
        {
            soulCount++;
            UIManager.Instance.UpdateSoulsCount(soulCount);
            Debug.Log($"Alma recogida. Total: {soulCount}");
        }
    }
    public bool HasKey()
    {
        // Busca en el inventario si existe un ítem de tipo Key
        return inventory.Exists(item => item.itemType == ItemType.Key);
    }
    public void UsePotion()
    {
        // Buscar una poción en el inventario
        ItemDataSO potion = inventory.Find(item => item.itemType == ItemType.Potion);
    
        if (potion != null)
        {
            // Curar al jugador con el valor de la poción (ej: 4 = un corazón)
            Heal(potion.effectValue);
        
            // Eliminar UNA poción del inventario
            inventory.Remove(potion);
        
            // Actualizar la UI del contador
            UIManager.Instance.UpdatePotionCount(PotionCount);
        
            Debug.Log("Poción usada. Salud restaurada en " + potion.effectValue);
        }
        else
        {
            Debug.Log("No tienes pociones.");
        }
    }

    
}