using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    PlayerAnimation playerAnimation;
    PlayerController controller;
    PlayerMovement movement;

	// Use this for initialization
	void Start () {
        playerAnimation = gameObject.GetComponent<PlayerAnimation>();
        controller = gameObject.GetComponent<PlayerController>();
        movement = gameObject.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CastSpell(Vector3 direction)
    {
        // Create a projectile for the spell and play an animation.
        LaunchProjectile(direction);
        playerAnimation.SpellAnimation();
    }

    public void LaunchProjectile(Vector3 hitPoint)
    {
        Debug.DrawLine(hitPoint, gameObject.transform.position, Color.red, 2f);
    }




    // Това е Coroutine, който при извикване кара играча да погледне към мястото, на което мишката е цъкната, след което да стреля.
    // Времето, за което това се случва е attackTime. То се изчислява по формула = 10 фрейма / attackSpeed.
    // Колкото повече attackSpeed има играчът, толкова по-малко време ще отнема на coroutine-a да се изпълни,
    // следователно играчът ще атакува по-бързо.
    // След изтичане на attackTime се извиква метод LaunchProjectile, който изстрелва projectile към целта.
    // Така сме сигурни, че играчът не може да стреля с гръб към целта.
    public IEnumerator AttackAtMouse()
    {
        Vector3 hitPoint = controller.GetMouseHit().point;
        float startTime = Time.time;
        float attackSpeed = 1f;
        float attackTime = Time.deltaTime * 10f / attackSpeed;

        while (Time.time - startTime < attackTime)
        {
            movement.StopMoving();
            movement.LookAt(hitPoint);
            yield return null;
        }
        LaunchProjectile(hitPoint);
        yield return null;
    }
}
