using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerCombat playerCombat;

    CharacterStats myStats;

	// Use this for initialization
	void Start () {
        playerCombat = PlayerController.instance.GetComponent<PlayerCombat>();
        myStats = this.GetComponent<CharacterStats>();
	}

    public override void Interact()
    {
        base.Interact();

        playerCombat.Attack(myStats);
        // The player attacks the stats of this object(the enemy).
    }


}
