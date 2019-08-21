using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterCombat))]
public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }

    PlayerMovement movement;
    PlayerCombat combat;
    public Interactable interactable;
    Interactable oldInteractable;
    public bool interactionInProcess = false;

    public RaycastHit GetMouseHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit;

        // Тук създаваме променлива от struct RaycastHit. Struct е различно от class.
        // То няма default constructor (с 0 параметъра), не може да наследява други struct-ове,
        // нито да бъде наследяван. Struct e value type, a class e reference type.

        // Създаваме лъч, който сочи от main camera-та до позицията на мишката в момента на извикване на метода.
        // После използваме методът Physics.Raycast, който проверява дали лъчът ray е уцелил нещо, което има collider.
        // Ако е уцелил, връща информация за този collision и я съхранява в променливата hit от тип RaycastHit.
    }


	// Use this for initialization
	void Start () {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
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

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(combat.AttackAtMouse(GetMouseHit()));
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.StopMoving(0.1f);
            movement.LookAt(hitPoint);
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Try to get component Interactable from the hit object.
            interactable = GetMouseHit().collider.GetComponent<Interactable>();

            // If interactable is not null, set object as new focus and start trying interaction.
            if (interactable != null) 
            {
                oldInteractable = interactable;

                movement.SetFocus(interactable);
                Debug.Log("FOKUSIRAHME " + interactable.name);

                if (interactionInProcess == false)
                {
                    interactionInProcess = true;
                    interactable.StartCoroutine("TryInteraction");
                }         
            }

            // If interactable is null, remove focus and stop trying interaction.
            else
            {
                movement.RemoveFocus();
                if (oldInteractable != null)
                {
                    interactionInProcess = false;
                    oldInteractable.StopCoroutine("TryInteraction");
                    
                }
            }
        }
	}
   
}
