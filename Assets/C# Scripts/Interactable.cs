﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] public float radius;
    PlayerController controller;

    // Callback функция, която визуализира радиуса на gameobject-a с class Interactable
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
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
            Interactable objectToInteractWith = controller.interactable;

            Collider[] objectsInRadius = Physics.OverlapSphere(objectToInteractWith.transform.position, radius);
            for (int i = objectsInRadius.Length - 1; i > 0; i--)
            {
                if (objectsInRadius[i].name == "Player")
                {
                    controller.Interact(objectToInteractWith);
                    continue;
                }
            }
               //Debug.Log("Daleche sum, she probvam pak sled 1 sekunda");
               yield return new WaitForSecondsRealtime(0.5f);
        }

      // ESSSKEEETTIIIIT! Какво ли не пробвах, за да направя така, че обектът, за който се следи за интеракция
      // да се променя, а не да се пази само първия, който съм цъкнал след стартиране на проекта.
      // Пробвах дори да работя с GameObjects вместо с Interactables и промених всеки от трите скрипта
      // да бачка с GameObject-и. После обаче осъзнах, че така голяма част от логиката на останалия код става useless.

      // Всичките мъки бяха разрешени, когато поставих objectToInteractWith в while цикъла.
      // Когато беше отвън, се взимаше веднъж, а след това се работеше само и единствено с нея.
      // Трябваше ми доста време да се сетя, а реално просто можеше да се замисля малко повече преди да действам.
      // Сега като е в цикъла, interactable обекта се обновява всеки път, когато нов цикъл бъде викнат, т.е. през 1s.
    }
}
