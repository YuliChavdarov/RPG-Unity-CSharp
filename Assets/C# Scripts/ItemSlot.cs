using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour {

    public Image image;

    // Благодарение на prefab-овете в Unity не се налага да търся поотделно всеки ItemIcon и да го раздавам
    // на съответния ItemSlot. Unity прави това като пусна на мястото на image в prefab-a обектът, от който
    // ще търси component Image - т.е. ItemIcon. Пускам ItemIcon, цъкам apply и готово. Много добре е измислено.

    Item item;

	// Use this for initialization
	void Start () {

		
	}

    public void AddItem(Item newItem)
    {
        item = newItem;

        image.sprite = item.properties.sprite;
        image.enabled = true;
    }

    public void RemoveItem()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
    }
}
