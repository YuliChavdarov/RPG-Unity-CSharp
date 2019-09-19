using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable {

    [SerializeField]
    private GameObject glowingOrbs;

    public override void Interact()
    {
        base.Interact();
        UIController.instance.ShowTeleportPopup();
        glowingOrbs.SetActive(true);
        // teleport UI pop-up
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}


    public void TeleportToTristram()
    {
        UIController.instance.HideTeleportPopup();
        Debug.Log("Teleporting to Tristram");
    }
}
