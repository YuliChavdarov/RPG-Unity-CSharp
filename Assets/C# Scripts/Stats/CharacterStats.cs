using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public Stat maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;
    //public Stat attackSpeed;

    void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(this.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }

    public void Heal(int heal)
    {
        currentHealth += heal;

        if (currentHealth >= maxHealth.GetValue())
        {
            currentHealth = maxHealth.GetValue();
        }
    }

    public virtual void Die()
    {
        // play death animation, drop loot, game over... Depending on the object that dies.
    }
}
