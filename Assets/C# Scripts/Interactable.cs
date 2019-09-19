using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] public float radius = 2f;

    // Callback функция, която визуализира радиуса на gameobject-a с class Interactable
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

    void Start()
    {
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + this);

        StopCoroutine("TryInteraction");
        PlayerController.instance.interactionInProcess = false;
        PlayerMovement.instance.RemoveFocus();
    }

    public IEnumerator TryInteraction()
    {
        //Debug.Log("Kolko puti stigam do tuka we moi");

        // Отговорът е 1 път, защото след като рутината се спре, тя помни на кой ред е била спряна.
        // В случая това става с .Interact методът. След като я извикам отново, рутината продължава от там.
        // Стига до continue, спира да върти проверката за objectsInRadius, връща 1 секунда Wait, след което
        // влиза отново в while цикъла. Т.е. така, както е написана в момента, всеки път когато бъде
        // извикана, тази рутина ще върти само в тялото на while цикъла. Нещата над него ще се
        // изпълнят веднъж, а нещата под него са unreachable.

        while (true)
        {
            Collider[] objectsInRadius = Physics.OverlapSphere(this.transform.position, radius);
            for (int i = objectsInRadius.Length - 1; i > 0; i--)
            {
                if (objectsInRadius[i].name == "Player")
                {
                    Interact();
                }
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
