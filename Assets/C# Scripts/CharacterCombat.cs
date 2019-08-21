using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    private float attackDelay = 0.6f;
    private float combatCooldown = 5f;
    private float lastAttackTime;

    public bool inCombat { get; private set; }

    public event System.Action OnAttack;
    // Basically delegate, който е void и не приема параметри. Ще го използвам като callback, който да вика
    // анимацията на character-a.

    void Start()
    {
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DealDamage(targetStats, attackDelay));
            if (OnAttack != null)
            {
                OnAttack();
            }
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    IEnumerator DealDamage(CharacterStats targetStats, float delay)
    {
        yield return new WaitForSeconds(delay);

        CharacterStats myStats = this.GetComponent<CharacterStats>();

        targetStats.TakeDamage(myStats.damage.GetValue());

        if (targetStats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
