using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public delegate void OnUpdateUI();
    public OnUpdateUI onUpdateUICallback;

    public static UIController instance;
    void Awake()
    {
        instance = this;
    }

    public bool chestMode;

    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject equipmentUI;
    [SerializeField]
    GameObject chestUI;

    [SerializeField]
    GameObject tooltip;

    Inventory inventory;
    ChestController chestController;
    Chest chest;
    EnemyHealthBarController enemyHealthBarController;

	void Start () {

        inventory = Inventory.instance;
        chestController = ChestController.instance;
        chest = FindObjectOfType<Chest>();
        enemyHealthBarController = GetComponentInChildren<EnemyHealthBarController>();

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
            HideTooltip();

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

        chest.StartCoroutine("PlayerNearChest");
    }

    public void CloseChest()
    {
        chestUI.SetActive(false);
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        chestMode = false;

        for (int i = 0; i < inventory.inventorySpace; i++)
        {
            inventory.slots[i].switchButton.interactable = false;
        }
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

    public void HideEnemyHealthBar()
    {
        enemyHealthBarController.gameObject.SetActive(false);
    }

    public void ShowEnemyHealthBar()
    {
        enemyHealthBarController.gameObject.SetActive(true);
    }

    public void ShowTooltip(ItemProperties properties, Vector3 position)
    {
        
        tooltip.transform.position = position;

        string title = string.Format("<color={0}>{1}</color>", properties.GetRarityColor(), properties.name);
        string description = string.Format("<i>{0}</i>", properties.description);
        tooltip.GetComponentInChildren<Text>().text = title + "\n" + description;
        tooltip.SetActive(true);
    }

    // Overload for EquipmentProperties. (Simple early bind/polymorphism) 
    // (Лол, чак сега научих, че overload-ването на методи се нарича early bind/polymorphism,
    // а използването на inheritance и virtual/override - late bind.)

    public void ShowTooltip(EquipmentProperties properties, Vector3 position)
    {
        string healthBonus = string.Empty;
        string armorBonus = string.Empty;
        string damageBonus = string.Empty;

        if (properties.healthModifier != 0)
        {
            healthBonus = "\n" + "Health Bonus: " + properties.healthModifier;
        }

        if (properties.armorModifier != 0)
        {
            armorBonus = "\n" + "Armor Bonus: " + properties.armorModifier;
        }

        if (properties.damageModifier != 0)
        {
            damageBonus = "\n" + "Damage Bonus: " + properties.damageModifier;
        }

        string title = string.Format("<color={0}>{1}</color>", properties.GetRarityColor(), properties.name);
        string description = string.Format("<i>{0}</i>", properties.description);
        tooltip.GetComponentInChildren<Text>().text = title + "\n" + description + healthBonus + armorBonus + damageBonus;
        tooltip.transform.position = position;
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}
