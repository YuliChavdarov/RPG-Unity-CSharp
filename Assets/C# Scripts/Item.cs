using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour {

    [SerializeField]
    public ItemProperties properties;

	void Start () {
        //inventoryImage.sprite = properties.sprite;
        //Debug.Log(inventoryImage.sprite);
	}

    public virtual void Use()
    {
        Debug.Log("Using " + properties.name);
    }
    //virtual означава, че може по-късно да бъде override-нат.
}
