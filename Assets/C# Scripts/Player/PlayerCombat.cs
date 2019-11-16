using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCombat : CharacterCombat {

    PlayerAnimator playerAnimator;
    PlayerMovement movement;

    [SerializeField]
    GameObject arrow;

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

    public void ShootArrow(Vector3 hitPoint)
    {
        Debug.DrawLine(hitPoint, gameObject.transform.position, Color.red, 2f);

        GameObject arrowLaunched = Instantiate<GameObject>(arrow, this.transform.position + new Vector3(0, 0.3f, 0f), Quaternion.identity);

        Physics.IgnoreCollision(arrowLaunched.GetComponent<CapsuleCollider>(), this.GetComponent<CapsuleCollider>());

        Rigidbody arrowBody = arrowLaunched.GetComponent<Rigidbody>();

        Vector3 direction = hitPoint - this.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrowLaunched.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        arrowBody.velocity = direction.normalized * 10;

        arrowLaunched.GetComponent<Arrow>().SetShooter(this);
    }

    public IEnumerator FireAtMouse(RaycastHit hit)
    {
        Equipment weapon = EquipmentController.instance.currentEquipment[2];
        if(weapon == null)
        {
            yield break;
        }
        if (weapon.name != "Bow")
        {
            yield break;
        }

        if (attackCooldown <= 0)
        {

        float startTime = Time.time;
        float attackTime = Time.deltaTime * 10f / attackSpeed;

        StartCoroutine(movement.StopMoving(attackTime));

        StartCoroutine(movement.LookAt(hit.point, attackTime));
        //while (Time.time - startTime < attackTime)
        //{
        //    movement.LookAt(hit.point);
        //    yield return null;
        //}

        attackCooldown = 1f / attackSpeed;
        inCombat = true;
        lastAttackTime = Time.time;

        ShootArrow(hit.point);
        }
    }
    // Това е Coroutine, който кара играча да погледне към мястото на цъкане на десен бутон, след което да стреля.
    // Времето, за което това се случва е attackTime. То се изчислява по формула = 10 фрейма / attackSpeed.
    // Колкото повече attackSpeed има играчът, толкова по-малко време ще отнема на coroutine-a да се изпълни,
    // следователно играчът ще атакува по-бързо.
    // След изтичане на attackTime се извиква метод LaunchProjectile, който изстрелва projectile към целта.
    // Така сме сигурни, че играчът не може да стреля с гръб към целта.

    public override void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DealDamage(targetStats, attackDelay));

            float attackTime = Time.deltaTime * 10f / attackSpeed;

            float startTime = Time.time;

            StartCoroutine(movement.StopMoving(attackTime));
            StartCoroutine(movement.LookAt(targetStats.transform.position,attackTime));

            if (onAttack != null)
            {
                onAttack();
            }
            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }
}
