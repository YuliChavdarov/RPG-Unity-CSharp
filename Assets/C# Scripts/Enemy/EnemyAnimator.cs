using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : CharacterAnimator {

    public AnimationClip combatIdle;
    protected override void Start()
    {
        base.Start();

        overrideController["CombatIdleReplacable"] = combatIdle;
    }
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
