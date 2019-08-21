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

    public SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;

    public Equipment oldItem = null;


    public delegate void OnEquipmentChanged(Equipment itemToEquip, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

	// Use this for initialization
	void Start () {
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
        equipmentSlot = GetComponentsInChildren<ItemSlot>();

        currentMeshes = new SkinnedMeshRenderer[numberOfSlots];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
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

        if (itemToEquip.mesh != null)
        {
            SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(itemToEquip.mesh);
            // Клонира mesh-a на itemToEquip, и го присвоява на newMesh.
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
 // Задава parent, bones и rootbone на клонирания mesh, които съвпадат с тези на targetMesh (Body-то на Player).

            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            currentMeshes[slotIndex] = newMesh;

            // If there is a mesh already, destroy it and then instantiate the new mesh.
        }

        onEquipmentChanged.Invoke(itemToEquip, oldItem);

    }

    void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            oldItem = currentEquipment[i];
            Inventory.instance.ReplaceItems(null, oldItem);
            currentEquipment[i] = null;

            equipmentSlot[i].icon.sprite = null;
            equipmentSlot[i].icon.enabled = false;

            Destroy(currentMeshes[i]);

            onEquipmentChanged.Invoke(null, oldItem);
        }

        if (UIController.instance.chestMode == true)
        {
            UIController.instance.onUpdateUICallback.Invoke();
        }

        SkinnedMeshRenderer[] equippedItems = targetMesh.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 1; i < equippedItems.Length; i++)
        {
            Destroy(equippedItems[i].gameObject);
        }

        // Малко тъпо решение, но работи, тъй като винаги Body (parent-a на equip-натите item-и) е с индекс 0.
        // Затова го пропускаме, като започваме директно от обекта на индекс 1. (i = 1)

        
    }
}
