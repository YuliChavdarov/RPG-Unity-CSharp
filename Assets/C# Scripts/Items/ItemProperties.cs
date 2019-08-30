using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ItemProperties")]
public class ItemProperties : ScriptableObject {

    public enum Rarity { Common, Rare, Epic, Legendary }

    public Rarity rarity;
    new public string name;
    public string description;
    public Sprite sprite;

    private string[] properties;

    void Start()
    {
        //foreach 
    }

    public string GetRarityColor()
    {
        switch (rarity)
        {
            case Rarity.Common:
                return "#ffffff";
            case Rarity.Rare:
                return "#499DF5";
            case Rarity.Epic:
                return "#DDA0DD";
            case Rarity.Legendary:
                return "#f7a820";
            default:
                return string.Empty;
        }
    }

    
}
