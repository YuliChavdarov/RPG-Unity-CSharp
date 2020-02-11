using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;

    GameObject target;
    NavMeshAgent agent;
    CharacterStats targetStats;
    CharacterCombat combat;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        target = PlayerController.instance.gameObject;
        targetStats = target.GetComponent<CharacterStats>();
        combat = this.GetComponent<CharacterCombat>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.transform.position, this.transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                combat.Attack(targetStats);
                LookAtTarget();
            }
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRotation, 0.1f);
    }
}
