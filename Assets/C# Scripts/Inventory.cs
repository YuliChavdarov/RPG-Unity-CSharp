using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {


    public static Inventory instance;
    void Awake()
    {
        instance = this;
    }
    // Това тук се нарича Singleton. Позволява ни да access-ваме обектът с class Inventory отвсякъде 
    // чрез Inventory.instance


    public InventoryController inventoryController;

    public Item[] items;

    public int firstFreeSlot;

    [SerializeField] public int inventorySpace;


	void Start () 
    {
        inventoryController = FindObjectOfType<InventoryController>();
        items = new Item[inventorySpace];
	}

    void Update()
    {

    }

    public bool TryPickup(Item item)
    {
        firstFreeSlot = 0;

        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                firstFreeSlot = i;
                break;
            }
        }

        if (firstFreeSlot < inventorySpace)
        {
            Debug.Log("Picking up " + item.name);
            items[firstFreeSlot] = item;

            inventoryController.AddItem(item, firstFreeSlot);

            return true;
        }
        else
        {
            return false;
            // Ако няма място в инвентара, върни false - т.е. кажи, че item-a не е бил взет.
        }
        
    }

    public void RemoveItem(Item item)
    {
        int slotOfItem = System.Array.IndexOf(items, item);
        items[slotOfItem] = null;
    }

    public void ReplaceItems(Item itemToEquip, Item oldItem)
    {
        int slotIndexInventory = System.Array.IndexOf(items, itemToEquip);
        inventoryController.ReplaceItems(itemToEquip, oldItem, slotIndexInventory);
    }
}
