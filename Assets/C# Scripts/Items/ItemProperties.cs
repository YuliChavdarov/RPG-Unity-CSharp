using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ItemProperties")]
public class ItemProperties : ScriptableObject {

    new public string name;
    public string description;
    public Sprite sprite;
}
