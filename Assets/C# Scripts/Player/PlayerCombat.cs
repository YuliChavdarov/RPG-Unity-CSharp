using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat {

    PlayerAnimator playerAnimator;
    PlayerMovement movement;

	// Use this for initialization
	void Start () {
        playerAnimator = gameObject.GetComponent<PlayerAnimator>();
        movement = gameObject.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Изключително важно е или да викаме base.Update(), или да нямаме void Update() в този скрипт, защото
        // иначе Update() от CharacterCombat няма да се изпълни. В случая чрез него се изчислява
        // attack cooldown-a.
        // tl;dr: Винаги при наследяване на класове проверявай Update методите и в parent-a, и в child-a.
    }

    public void CastSpell(Vector3 direction)
    {
        // Placeholder.
        //LaunchProjectile(direction);
        playerAnimator.SpellAnimation();
    }

    public void Placehold(){}

    public void LaunchProjectile(RaycastHit hit, float attackTime)
    {
        Debug.DrawLine(hit.point, gameObject.transform.position, Color.red, 2f);

       // movement.StopMoving(attackTime);

        Enemy enemy = hit.collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
            Attack(enemyStats);
        }
    }
    public IEnumerator FireAtMouse(RaycastHit hit)
    {
        float startTime = Time.time;
        float attackTime = Time.deltaTime * 10f / attackSpeed;

        while (Time.time - startTime < attackTime)
        {
            //movement.StopMoving(0.01f);
            movement.LookAt(hit.point);
            yield return null;
        }
        LaunchProjectile(hit, attackTime);
    }
    // Това е Coroutine, който кара играча да погледне към мястото на цъкане на десен бутон, след което да стреля.
    // Времето, за което това се случва е attackTime. То се изчислява по формула = 10 фрейма / attackSpeed.
    // Колкото повече attackSpeed има играчът, толкова по-малко време ще отнема на coroutine-a да се изпълни,
    // следователно играчът ще атакува по-бързо.
    // След изтичане на attackTime се извиква метод LaunchProjectile, който изстрелва projectile към целта.
    // Така сме сигурни, че играчът не може да стреля с гръб към целта.
}
