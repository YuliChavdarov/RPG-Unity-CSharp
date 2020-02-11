using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    public static ChestController instance;
    void Awake()
    {
        instance = this;
    }

    public Item[] chestItems;

    public ItemSlot[] chestSlots;

    public int firstFreeSlot;

    [SerializeField]
    public int chestSpace;

	// Use this for initialization
	void Start () {
        chestSlots = GetComponentsInChildren<ItemSlot>();
        chestItems = new Item[chestSpace];
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public int GetFirstFreeSlotIndex()
    {
        for (int i = 0; i < chestSpace; i++)
        {
            if (chestItems[i] == null)
            {
                return i;
            }
        }
        return 0;
        // fix later
    }

    public void ReturnToInventory(Item item)
    {
        Inventory.instance.TryPickup(item);
        int slotOfItem = System.Array.IndexOf(chestItems, item);
        chestItems[slotOfItem] = null;
        chestSlots[slotOfItem].ClearSlot();

        UIController.instance.onUpdateUICallback.Invoke();
    }
}
