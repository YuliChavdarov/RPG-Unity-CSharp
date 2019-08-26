using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public static LootManager instance;

    public Item[] itemsLevel3;
    public Item[] itemsLevel5;

    public Dictionary<int, Item[]> lootDict = new Dictionary<int, Item[]>();
    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        lootDict.Add(3, itemsLevel3);
        lootDict.Add(5, itemsLevel5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropLoot(int level, Transform source)
    {
        Item[] wantedArray = lootDict[level];
        if (wantedArray != null)
        {
            int randomIndex = Random.Range(0, wantedArray.Length);
            DropItem(wantedArray[randomIndex], source);
        }

        else
        {
            Debug.Log("no items for this level enemy, or the array of items is not found");
        }
    }


    public void DropItem(Item item, Transform source)
    {
        string itemName = item.name;
        float yAxisOffset = 0.1f;
        Vector3 dropPosition = new Vector3(source.position.x, source.position.y + yAxisOffset, source.position.z);

        GameObject droppedItem = Instantiate<GameObject>(item.gameObject, dropPosition, Quaternion.identity);
        if (item.gameObject.activeSelf == false)
        {
            Destroy(item.gameObject);
            //Ако същият предмет е бил взет преди, старият gameobject, който вече не е активен, се destroy-ва.
            //Ако същият предмет е дропнат, но не е взет, старият не се destroy-ва, а просто се дропва още един.
        }
        droppedItem.name = itemName;
        droppedItem.SetActive(true);
    }
}
