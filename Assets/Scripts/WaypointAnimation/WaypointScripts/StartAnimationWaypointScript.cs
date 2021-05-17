using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationWaypointScript : WaypointScript
{
    public Animator animator;
    public string startAnimationStateName;
    public string finishedAnimationStateName = "Idle";
    public bool waitForAnimationToFinish = true;

    protected override IEnumerator AnimateInstance(AnimatableEntity subject, float speed)
    {
        animator.Play(startAnimationStateName);

        bool firstLoop = true;

        if (waitForAnimationToFinish)
        {
            while (firstLoop || !animator.GetCurrentAnimatorStateInfo(0).IsName(finishedAnimationStateName))
            {
                firstLoop = false;
                yield return null;
            }
        }
    }

    protected override void DrawGizmosInstance(Transform prevWaypoint) {}
}
