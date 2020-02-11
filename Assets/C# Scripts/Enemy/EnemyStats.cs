using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    public int level;
    public override void Die()
    {
        Destroy(GetComponent<CharacterCombat>());
        Destroy(GetComponent<Enemy>());
        GetComponent<EnemyAnimator>().PlayDeathAnimation();
        LootManager.instance.DropLoot(level, this.transform);

        UIController.instance.HideEnemyHealthBar();
        Destroy(gameObject, 2f);
    }
}
