using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    GameObject inventoryUI;
    GameObject equipmentUI;

	// Use this for initialization
	void Start () {

        inventoryUI = GetComponentInChildren<Inventory>().gameObject;
        equipmentUI = EquipmentController.instance.gameObject;
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
}
