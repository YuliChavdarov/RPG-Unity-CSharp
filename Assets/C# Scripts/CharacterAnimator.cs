using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    protected NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;

    protected float speedPercent;
    protected AnimatorOverrideController overrideController;
    public AnimationClip[] currentAttackAnimationSet;
    public AnimationClip[] defaultAttackAnimationSet;

    

	protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        

        currentAttackAnimationSet = defaultAttackAnimationSet;

        combat.onAttack += PlayAttackAnimation;
	}

    protected virtual void Update()
    {
        speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("animationSpeedPercent", speedPercent, 0.1f, Time.deltaTime);

        animator.SetBool("inCombat", combat.inCombat);

        //Създаваме променлива speedPercent, която изчислява скоростта спрямо максималната скорост в дадения момент.
        //Присвояваме я на Blend Tree-то в AnimationController. Това Blend Tree се състои от три анимации - Idle, Walk, Run.
        //Когато speedPercent-a е нисък (т.е. в началото докато засили и в края преди да спре), се пуска анимацията Walk.
        //Когато стойността е 0 се пуска Idle, а когато е близо до 1 (т.е. моментната скорост е близка до максималната),
        //се пуска Run.
	}

    protected virtual void PlayAttackAnimation()
    {
        int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
        overrideController["AttackReplacable"] = currentAttackAnimationSet[attackIndex];
        animator.SetTrigger("attack");

        // Когато някой character атакува, избираме random анимация от currentAnimationSet-а, която
        // се изпълнява вместо AttackReplacable.
    }
}
