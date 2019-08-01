using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {

    [SerializeField]
    public EquipmentProperties equipmentProperties;

    public SkinnedMeshRenderer mesh;

    public EquipmentSlot equipSlot;
}

public enum EquipmentSlot { Head, Chest, Weapon, Shield, Pants, Boots, Belt, Amulet, Ring1, Ring2}