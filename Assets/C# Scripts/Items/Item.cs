using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : Interactable {

    [SerializeField]
    public ItemProperties properties;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    public void PickUp()
    {
        Item item = this.GetComponent<Item>();
        bool wasPickedUp = Inventory.instance.TryPickup(item);
        if (wasPickedUp == true)
        {
            item.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Your inventory is full!");
        }
    }
}
