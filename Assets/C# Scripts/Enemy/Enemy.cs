using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : Interactable {

    PlayerCombat playerCombat;

    CharacterStats myStats;

    EnemyHealthBar healthBar;

	// Use this for initialization
	void Start () {
        playerCombat = PlayerController.instance.GetComponent<PlayerCombat>();
        myStats = this.GetComponent<CharacterStats>();
        healthBar = EnemyHealthBar.instance;
	}

    public override void Interact()
    {
        base.Interact();

        playerCombat.Attack(myStats);
        // The player attacks the stats of this object(the enemy).
    }

    void OnMouseOver()
    {
        UIController.instance.ShowEnemyHealthBar();
        healthBar.hoveredEnemy = this;
    }

    void OnMouseExit()
    {
        StartCoroutine(HideHealthBar());
    }

    IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(5f);
        UIController.instance.HideEnemyHealthBar();
        yield break;

        //If player haven't hovered over an enemy in the last 5 seconds, hide the EnemyHealthBar.
    }

}
