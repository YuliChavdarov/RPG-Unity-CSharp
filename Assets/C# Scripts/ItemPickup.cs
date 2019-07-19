using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}

    public void PickUp(ItemPickup itemToPickUp)
    {
        Item item = itemToPickUp.GetComponent<Item>();
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

    // Научих как става референцийка към обект без да го търсиш с FindObjectOfType или да се занимаваш с масиви и т.н.
    // Просто даваш на всеки обект, който искаш да можеш да Pickup-неш, class Pickup примерно.
    // След това пробваш да вземеш component от тип Pickup, и ако стане, викаш някакъв метод. Ако този метод
    // бачка с друг компонент на обекта, викаш другия компонент с GetComponent<>() и го подаваш.
}
