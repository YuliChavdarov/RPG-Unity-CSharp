using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;

    void Awake()
    {
        instance = this;
    }

    public int Health { get; private set; }
    public int Armor { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToHealth(int healthBonus)
    {
        Health += healthBonus;
    }

    public void AddToArmor(int armorBonus)
    {
        Armor += armorBonus;
    }
}
