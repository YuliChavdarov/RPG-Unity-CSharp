using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour {
    
    NavMeshAgent playerAgent;
    Interactable focus;

	// Use this for initialization
	void Start () {
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        // Ако имаме фокусиран обект, извикваме метод MoveToDestination, на който даваме
        // позицията на обект focus. Дори и фоксураният обект да се движи, неговата позиция се взема на всеки фрейм.
        // Това не е най-оптималното възможно решение performance-wise, но е най-лесното.
        if (focus != null)
        {
            Vector3 focusPosition = focus.transform.position;
            MoveToDestination(focusPosition);
            playerAgent.stoppingDistance = focus.radius * 0.8f;
            LookAt(focusPosition);
        }
    }
    
    public void MoveToDestination(Vector3 destination)
    {
        playerAgent.SetDestination(destination);
    }

    public void StopMoving()
    {
        playerAgent.SetDestination(gameObject.transform.position);
    }

    public void SetFocus(Interactable newFocus)
    {
       focus = newFocus;
    }

    public void RemoveFocus()
    {
        focus = null;
    }

    public void LookAt(Vector3 target)
    {
        Vector3 direction = (target - gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRotation, 0.1f);

        // Quaternion е struct, който се използва за манипулиране на ротации. методът .Slerp интерполира (прави smooth)
        // промяната между настоящата ротация и желаната ротация. Скоростта на интерполацията се определя от последния
        // параметър, който е float със стойност между 0 и 1.
    }



 
}
