using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour {

    public Image icon;
    // Благодарение на prefab-овете в Unity не се налага да търся поотделно всеки ItemIcon и да го раздавам
    // на съответния ItemSlot. Unity прави това като пусна на мястото на image в prefab-a обектът, от който
    // ще търси component Image - т.е. ItemIcon. Пускам ItemIcon, цъкам apply и готово. Много добре е измислено.

    public Button removeButton;
    public Button switchButton;
    public Button useButton;
    Item item;

	// Use this for initialization
	void Start () {
        //removeButton.interactable = false;
        icon.enabled = false;
	}

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.properties.sprite;
            icon.enabled = true;
            removeButton.interactable = true;
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        switchButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.DropItem(item);
        Inventory.instance.RemoveItem(item);
        ClearSlot();
    }
    public void OnSwitchButton()
    {
        Inventory inventory = GetComponentInParent<Inventory>();
        ChestController chest = GetComponentInParent<ChestController>();

        if (inventory != null)
        {
            Inventory.instance.PutInChest(item);
            Inventory.instance.RemoveItem(item);
            ClearSlot();
        }
       
        else if (chest != null)
        {
            ChestController.instance.ReturnToInventory(item);
        }
        else
        {
            Debug.Log("The item is not in the inventory, nor in the chest.");
        }
    }


    public void UseItem()
    {
        Equipment isEquipment = item.GetComponent<Equipment>();

        if (isEquipment != null)
        {
            EquipmentController.instance.Equip(isEquipment);

            if (EquipmentController.instance.oldItem == null)
            {
                Inventory.instance.RemoveItem(item);
                ClearSlot();
            }
        }

        else
        {
            Debug.Log("Using " + item.properties.name);
            Inventory.instance.RemoveItem(item);
            ClearSlot();
        }
    }

    
}
