using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : CharacterAnimator {

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
    // Struct, който държи анимациите, свързани със съответното оръжие. Самите анимации се вкарват в масива
    // директно от Unity editor-a.
    // Всички WeaponAnimations от своя страна се държат от речник weaponAnimationsDict, който има ключ
    // от тип Equipment (оръжието), и стойност - масив от анимации, съответстващи на това оръжие.


    public WeaponAnimations[] weaponAnimations;

    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    float spellCastSpeed;
    float fasterCastRate = 0.8f;

    [SerializeField]
    EquipmentController equipmentController;

    [SerializeField]
    private AnimationClip[] defaultAttackSet;
	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
        equipmentController.onEquipmentChanged += UpdateAvatar;
        AttackAnimationSet = defaultAttackSet;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SpellAnimation()
    {
        spellCastSpeed = 1 / fasterCastRate;
        animator.SetFloat("spellCastSpeed", spellCastSpeed, 0.1f, Time.deltaTime);
        //animator.Play("CastSpell");
    }


    void UpdateAvatar(Equipment itemToEquip, Equipment oldItem)
    {
        if (itemToEquip != null && itemToEquip.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);

            if (weaponAnimationsDict.ContainsKey(itemToEquip))
            {
                AttackAnimationSet = weaponAnimationsDict[itemToEquip];
            }
            // Когато equip-ваме оръжие, проверяваме дали се съдържа в речника с анимации. Ако го има,
            // присвояваме на currentAnimationSet съответстващия му масив от анимации.
        
        }
        if (oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon && itemToEquip == null)
        {
            animator.SetLayerWeight(1, 0);
            // Releases right hand (weight = 0), if the player wants to unequip an item from the weapon slot.
        }


        if (itemToEquip != null && itemToEquip.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        if (oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield && itemToEquip == null)
        {
            animator.SetLayerWeight(2, 0);
            // Releases left hand (weight = 0), if the player wants to unequip an item from the shield slot.
        }

        if (itemToEquip == null)
        {
            AttackAnimationSet = defaultAttackSet;
        }
    }
}
