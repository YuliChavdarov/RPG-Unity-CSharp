using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalManager : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = PlayerController.instance.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportTo(Portal destinationPortal)
    {
        UIController.instance.HideTeleportPopup();
        Debug.Log("Teleporting to " + destinationPortal.name);
        player = PlayerController.instance.gameObject;
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        player.transform.position = destinationPortal.transform.position;
        agent.enabled = true;
    }
}
