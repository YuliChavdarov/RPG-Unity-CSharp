using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : Interactable {

    [SerializeField]
    private GameObject glowingOrbs;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        base.Interact();
        UIController.instance.ShowTeleportPopup();
        glowingOrbs.SetActive(true);
    }
}
