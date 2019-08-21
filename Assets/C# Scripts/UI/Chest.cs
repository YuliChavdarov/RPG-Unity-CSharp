using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

    public override void Interact()
    {
        base.Interact();
        UIController.instance.OpenChest();
    }

    public IEnumerator PlayerNearChest()
    {
        bool playerIsNear = true;

        while (true)
        {
            if (playerIsNear == true)
            {
                Collider[] objectsInRadius = Physics.OverlapSphere(this.transform.position, radius);

                for (int i = objectsInRadius.Length - 1; i > 0; i--)
                {
                    if (objectsInRadius[i].name == "Player")
                    {
                        playerIsNear = true;
                        break;
                    }
                    playerIsNear = false;
                }
                yield return new WaitForSecondsRealtime(0.15f);
            }

            else
            {
                UIController.instance.CloseChest();
                yield break;
            }
        }
    }
    // Checks every 0.15s if player is in interaction radius of chest. If not, the chest gets closed.
}
