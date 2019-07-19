using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    GameObject inventoryObject;
    Inventory inventory;

    public ItemSlot[] slots;

	// Use this for initialization
	void Start () {

        inventoryObject = GetComponentInChildren<Inventory>().gameObject;
        inventory = Inventory.instance;
        slots = inventory.GetComponentsInChildren<ItemSlot>();


        inventory.onItemChangedCallback += UpdateUI;

        // По този начин subscribe-ваме delegate-a onItemChangedCallback към метод UpdateUI.
        // Това означава, че всеки път когато извикаме onItemChangedCallback, ще се извършва този метод.
        // Готиното е, че можем да subscribe-нем още методи към този delegate callback, и всеки път, когато
        // бъде извикан, ще се извършват няколко метода едновременно.
        // Друго полезно приложение на delegate е, че чрез него можем да задаваме методи като параметри на други методи.
    }
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventory.inventorySpace; i++)
        {
            if (i < inventory.items.Count) 
            {
                slots[i].AddItem(inventory.items[i]);
            }
            // Всеки път, когато се извика UpdateUI, настаняваме item-a в слот с номер,
            // равен на броя на item-ите, които имаме в Inventory List<Item> items.
        }
    }

    public void ToggleInventory()
    {
        if (inventoryObject.activeSelf == false)
        {
            inventoryObject.SetActive(true);
        }
        else
        {
            inventoryObject.SetActive(false);
        }
    }
}
