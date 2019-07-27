using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {

    public int defense;
    public int damage;

    public EquipmentSlot equipSlot;
}

public enum EquipmentSlot { Head, Chest, Weapon, Shield, Gloves, Boots, Belt, Amulet, Ring1, Ring2}