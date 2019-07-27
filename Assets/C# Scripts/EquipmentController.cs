using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{

    #region Singleton
    public static EquipmentController instance;
 
    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;
    public ItemSlot[] equipmentSlot;

    public Equipment oldItem = null;

	// Use this for initialization
	void Start () {
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
        equipmentSlot = GetComponentsInChildren<ItemSlot>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Equip(Equipment itemToEquip)
    {
        int slotIndex = (int)itemToEquip.equipSlot;

        oldItem = currentEquipment[slotIndex];

        if (oldItem != null)
        {
            Inventory.instance.ReplaceItems(itemToEquip, oldItem);
            Debug.Log("vrushtam ti stariq item: " + oldItem.name);
        }

        Debug.Log("Equipvam noviq item: " + itemToEquip.name);
        currentEquipment[slotIndex] = itemToEquip;

        equipmentSlot[slotIndex].icon.sprite = itemToEquip.properties.sprite;
        equipmentSlot[slotIndex].icon.enabled = true;
    }
}
