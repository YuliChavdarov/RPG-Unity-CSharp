using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public CharacterCombat shooter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetShooter(CharacterCombat newShooter)
    {
        shooter = newShooter;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.collider.name);

        Enemy enemy = collision.collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
            enemyStats.TakeDamage(shooter.GetComponent<CharacterStats>().damage.GetValue());
        }
        Destroy(this.gameObject);
    }
}
