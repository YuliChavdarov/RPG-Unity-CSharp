using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public delegate void OnUpdateUI();
    public OnUpdateUI onUpdateUICallback;

    public static UIController instance;

    public bool chestMode;

    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject equipmentUI;
    [SerializeField]
    GameObject chestUI;

    Inventory inventory;
    ChestController chestController;
	// Use this for initialization
	void Start () {

        inventory = Inventory.instance;
        chestController = ChestController.instance;

        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        chestUI.SetActive(false);

        // Важно е този скрипт(UIController) да се извърши след Inventory, EquipmentController и ChestController,
        // защото иначе при отваряне на инвентара или chest-a, ще дава Index out of range,
        // понеже масивите items[], currentEquipment[] и chestItems[] няма да са инициализирани.
        // Приоритетът на изпълнение на скриптовете може да се настройва ръчно от меню Script Execution Order.

        onUpdateUICallback += UpdateChestUI;
    }
	
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (chestMode == false)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                equipmentUI.SetActive(!equipmentUI.activeSelf);
            }
            // Toggle-ва инвентара при цъкане на бутон с име Inventory, зададен от Edit > ProjectSettings > Input.
            // В случая съм задал клавиш "i".
        }
    }

    public void OpenChest()
    {
        chestUI.SetActive(true);
        inventoryUI.SetActive(true);
        equipmentUI.SetActive(true);
        chestMode = true;

        onUpdateUICallback.Invoke();

        
    }

    public void CloseChest()
    {
        chestUI.SetActive(false);
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        chestMode = false;
    }

    public void UpdateChestUI()
    {
        for (int i = 0; i < inventory.inventorySpace; i++)
        {
            if (inventory.items[i] != null)
            {
                inventory.slots[i].switchButton.interactable = true;
                inventory.slots[i].removeButton.interactable = true;
                inventory.slots[i].useButton.interactable = true;
            }
        }

        for (int i = 0; i < chestController.chestSpace; i++)
        {
            if (chestController.chestItems[i] != null)
            {
                chestController.chestSlots[i].switchButton.interactable = true;
                chestController.chestSlots[i].removeButton.interactable = false;
                chestController.chestSlots[i].useButton.interactable = false;
            }
        }
    }
}
