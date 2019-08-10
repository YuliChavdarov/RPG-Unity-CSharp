using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour {


    NavMeshAgent playerAgent;
    Animator playerAnimator;
    float speedPercent;
    float spellCastSpeed;
    float fasterCastRate = 0.8f;

    [SerializeField]
    EquipmentController equipmentController;

	// Use this for initialization
	void Start () {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponentInChildren<Animator>();

        equipmentController.onEquipmentChanged += UpdateAvatar;
    }
	
	// Update is called once per frame
	void Update () {
        speedPercent = playerAgent.velocity.magnitude / playerAgent.speed;
        playerAnimator.SetFloat("animationSpeedPercent", speedPercent, 0.1f, Time.deltaTime);

        //Създаваме променлива speedPercent, която изчислява скоростта спрямо максималната скорост в дадения момент.
        //Присвояваме я на Blend Tree-то в AnimationController. Това Blend Tree се състои от три анимации - Idle, Walk, Run.
        //Когато speedPercent-a е нисък (т.е. в началото докато засили и в края преди да спре), се пуска анимацията Walk.
        //Когато стойността е 0 се пуска Idle, а когато е близо до 1 (т.е. моментната скорост е близка до максималната),
        //се пуска Run.
	}

    public void SpellAnimation()
    {
        spellCastSpeed = 1 / fasterCastRate;
        playerAnimator.SetFloat("spellCastSpeed", spellCastSpeed, 0.1f, Time.deltaTime);
        //playerAnimator.Play("CastSpell");
    }


    void UpdateAvatar(Equipment itemToEquip, Equipment oldItem)
    {
        if (itemToEquip != null && itemToEquip.equipSlot == EquipmentSlot.Weapon)
        {
            playerAnimator.SetLayerWeight(1, 1);
        // Sets weight to 1 on layer with index 1 (Right Hand).
        }
        if (oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon && itemToEquip == null)
        {
            playerAnimator.SetLayerWeight(1, 0);
        // Releases right hand (weight = 0), if the player wants to unequip an item from the weapon slot.
        }


        if (itemToEquip != null && itemToEquip.equipSlot == EquipmentSlot.Shield)
        {
            playerAnimator.SetLayerWeight(2, 1);
        // Sets weight to 1 on layer with index 2 (Left hand).
        }
        if (oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield && itemToEquip == null)
        {
            playerAnimator.SetLayerWeight(2, 0);
            // Releases left hand (weight = 0), if the player wants to unequip an item from the shield slot.
        }
    }
}
