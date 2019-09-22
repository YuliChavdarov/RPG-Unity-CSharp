using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalManager : MonoBehaviour {


    public Transform forestPortal;
    public Transform tristramPortal;

    GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportToTristram()
    {
        UIController.instance.HideTeleportPopup();
        Debug.Log("Teleporting to Tristram");
        player = PlayerController.instance.gameObject;
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        player.transform.position = tristramPortal.position;
        agent.enabled = true;
    }
}
