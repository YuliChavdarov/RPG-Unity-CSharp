using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	void Start () 
    {
        EquipmentController.instance.onEquipmentChanged += UpdateStatsOnEquipmentChanged;
	}

    public void UpdateStatsOnEquipmentChanged(Equipment itemToEquip, Equipment oldItem)
    {
        if (itemToEquip != null)
        {
            maxHealth.AddModifier(itemToEquip.equipmentProperties.healthModifier);
            armor.AddModifier(itemToEquip.equipmentProperties.armorModifier);
            damage.AddModifier(itemToEquip.equipmentProperties.damageModifier);
        }

        if (oldItem != null)
        {
            maxHealth.RemoveModifier(oldItem.equipmentProperties.healthModifier);
            Heal(0); // За да преизчисли currentHealth-a след като махнем modifier-a.
            armor.RemoveModifier(oldItem.equipmentProperties.armorModifier);
            damage.RemoveModifier(oldItem.equipmentProperties.damageModifier);
        }
    }
}
