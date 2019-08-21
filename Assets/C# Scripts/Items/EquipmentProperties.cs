using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "EquipmentProperties")]
public class EquipmentProperties : ItemProperties 
{
    public int healthModifier;
    public int armorModifier;
    public int damageModifier;
}
