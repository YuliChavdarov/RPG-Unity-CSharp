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


    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;


    public List<Item> items = new List<Item>();

    [SerializeField] public int inventorySpace;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    public bool TryPickup(Item item)
    {
        if (items.Count < inventorySpace)
        {
            Debug.Log("Picking up " + item.name);
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            // Ако този callback има някаква функция, извикай го да я извърши.

            return true;
        }
        else
        {
            return false;
            // Ако няма място, върни false, т.е. кажи, че item-a не е бил взет.
        }
        
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }


    // Това, което следва да направя е да създам бутон, който да премахва item-ите от List<Item> items
    // т.е. да го вържа към inventory.RemoveItem, a в InventoryController.UpdateUI() да извикам ItemSlot.RemoveItem().
    // По този начин хем ще го премахна от list-a с item-и, хем ще го махна от UI-я на инвентара.
    // В Unity editor-a имаше опция onclick да извиква еди кой си метод. Ще трябва да ги вържа чрез тази опция.
}
