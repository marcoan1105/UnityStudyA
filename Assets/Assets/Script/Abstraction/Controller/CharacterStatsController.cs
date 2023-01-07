using Assets.Assets.Script.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ButtonConfiguration.Damage))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if (OnHealthChanged != null)
        {
            OnHealthChanged(MaxHealth, CurrentHealth);
        }

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // die in some way
        // This method is meat to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
