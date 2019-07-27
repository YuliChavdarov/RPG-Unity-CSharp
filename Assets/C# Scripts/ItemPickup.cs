using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
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
