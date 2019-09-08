using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : CharacterAnimator {

    [SerializeField]
    private AnimationClip combatIdle;
    [SerializeField]
    private AnimationClip idle;
    [SerializeField]
    private AnimationClip walk;
    [SerializeField]
    private AnimationClip run;
    protected override void Start()
    {
        base.Start();

        overrideController["CombatIdleReplacable"] = combatIdle;
        overrideController["Idle"] = idle;
        overrideController["Walk"] = walk;
        overrideController["Run"] = run;
    }
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
