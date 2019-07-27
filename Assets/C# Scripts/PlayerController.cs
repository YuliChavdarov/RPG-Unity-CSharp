using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    PlayerMovement movement;
    PlayerCombat combat;
    IEnumerator tryInteraction;
    public Interactable interactable;

    public RaycastHit GetMouseHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit;

        // Тук създаваме променлива от struct RaycastHit. Struct е съвсем различно от class.
        // То няма default constructor (с 0 параметъра), не може да наследява други struct-ове,
        // нито да бъде наследяван. Struct e value type, a class e reference type.

        // Създаваме лъч, който сочи от main camera-та до позицията на мишката в момента на извикване на метода.
        // После използваме методът Physics.Raycast, който проверява дали лъчът ray е уцелил нещо, което има collider.
        // Ако е уцелил, връща информация за този collider и я съхранява в променливата hit от тип RaycastHit.
    }

    public void Interact(Interactable interactable)
    {
         //Checks if the interactable can be picked up.
        ItemPickup itemToPickup = interactable.GetComponent<ItemPickup>();
       if (itemToPickup != null)
        {
            itemToPickup.PickUp();
        }
        else 
        { 
               // TO DO: Use method that uses Interactable as a parameter.
            //Interactable.InteractWith(interactable);
           Debug.Log("Interacting with something that can't be picked up.");
        }
        StopCoroutine(tryInteraction);
    }


	// Use this for initialization
	void Start () {
        movement = FindObjectOfType<PlayerMovement>();
        combat = FindObjectOfType<PlayerCombat>();
        tryInteraction = FindObjectOfType<Interactable>().TryInteraction();
	}
	
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
            
            // Благодарение на този ред не позволяваме на играчът да отчита кликовете върху инвентара
            // като дестинация, към която да се запъти.
        }

        Vector3 hitPoint = GetMouseHit().point;

        if (Input.GetMouseButton(0))
        {
            movement.MoveToDestination(hitPoint);
        }

        if (Input.GetMouseButton(1))
        {
            StartCoroutine(combat.AttackAtMouse());
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.StopMoving();
            movement.LookAt(hitPoint);
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Try to get component Interactable from the hit object.
            interactable = GetMouseHit().collider.GetComponent<Interactable>();

            // If interactable is not null, set object as new focus and start trying interaction.
            if (interactable != null) 
            {
                movement.SetFocus(interactable);
                Debug.Log("FOKUSIRAHME " + interactable.name);
                StartCoroutine(tryInteraction);
            }

            // If interactable is null, remove focus and stop trying interaction.
            else
            {
                movement.RemoveFocus();
                StopCoroutine(tryInteraction);
            }
        }
	}
   
}
