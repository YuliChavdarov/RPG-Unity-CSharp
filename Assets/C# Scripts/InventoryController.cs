using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    GameObject inventoryUI;
    GameObject equipmentUI;
    Inventory inventory;

    public ItemSlot[] slots;

	// Use this for initialization
	void Start () {

        inventoryUI = GetComponentInChildren<Inventory>().gameObject;
        equipmentUI = EquipmentController.instance.gameObject;
        inventory = Inventory.instance;
        slots = inventory.GetComponentsInChildren<ItemSlot>();
    }
	
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            // Toggle-ва инвентара при цъкане на бутон с име Inventory, зададен от Edit > ProjectSettings > Input.
            // В случая съм задал клавиш "i".
        }
    }

    public void AddItem(Item item, int slotIndexInventory)
    {
         slots[slotIndexInventory].AddItem(item);
    }

    public void ReplaceItems(Item itemToEquip, Item oldItem, int slotIndexInventory)
    {
        Debug.Log("iska da go sloji v slot nomer: " + slotIndexInventory);
        inventory.items[slotIndexInventory] = oldItem;
        slots[slotIndexInventory].AddItem(oldItem);
        
        
    }
}
