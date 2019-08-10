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


    public Item[] items;

    public ItemSlot[] slots;

    public int firstFreeSlot;

    [SerializeField] public int inventorySpace;

	void Start () 
    {
        slots = GetComponentsInChildren<ItemSlot>();
        items = new Item[inventorySpace];
	}

    void Update()
    {
    }

    public bool TryPickup(Item item)
    {
        firstFreeSlot = GetFirstFreeSlotIndex();

        if (firstFreeSlot < inventorySpace)
        {
            Debug.Log("Picking up " + item.name);
            items[firstFreeSlot] = item;

            slots[firstFreeSlot].AddItem(item);
            return true;
        }
        else
        {
            return false;
            // Ако няма място в инвентара, върни false - т.е. кажи, че item-a не е бил взет.
        }
    }

    public int GetFirstFreeSlotIndex()
    {
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                return i;
            }
        }
        return 0;
        // fix later
    }

    public void RemoveItem(Item item)
    {
        int slotOfItem = System.Array.IndexOf(items, item);
        items[slotOfItem] = null;
    }

    public void ReplaceItems(Item itemToEquip, Item oldItem)
    {
        int slotIndexInventory = System.Array.IndexOf(items, itemToEquip);
        Debug.Log("iska da go sloji v slot nomer: " + slotIndexInventory);
        items[slotIndexInventory] = oldItem;
        slots[slotIndexInventory].AddItem(oldItem);
    }

    public void DropItem(Item item)
    {
        string itemName = item.name;

        PlayerController player = FindObjectOfType<PlayerController>();
        float yAxisOffset = 0.1f; 
        Vector3 dropPosition = new Vector3(player.transform.position.x, player.transform.position.y + yAxisOffset, player.transform.position.z);
        
        GameObject droppedItem = Instantiate<GameObject>(item.gameObject, dropPosition, Quaternion.identity);
        Destroy(item.gameObject);
        droppedItem.name = itemName;
        droppedItem.SetActive(true);
    }

    public bool PutInChest(Item item)
    {
        firstFreeSlot = ChestController.instance.GetFirstFreeSlotIndex();

        if (firstFreeSlot < ChestController.instance.chestSpace)
        {
            Debug.Log("Picking up " + item.name);
            ChestController.instance.chestItems[firstFreeSlot] = item;

            ChestController.instance.chestSlots[firstFreeSlot].AddItem(item);

            UIController.instance.onUpdateUICallback.Invoke();

            return true;
        }
        else
        {
            return false;
            // Ако няма място в инвентара, върни false - т.е. кажи, че item-a не е бил взет.
        }
    }
}
