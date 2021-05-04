using System.Collections;
using UnityEngine;

public abstract class WaypointScript : MonoBehaviour
{
    public WaypointScript WaypointToWaitOn;

    // Used for the animation blocks after a puzzle animation
    [Header("Makes the next coding block highlighted in animation")]
    public bool GoToNextCodingBlock;
    // The amount of coding blocks to progress when GoToNextCodingBlock is set to true
    public int CodingBlockSize = 1;

    private AnimationTrigger _animationTrigger;

    [HideInInspector]
    public bool IsDone = false;

    private void Awake()
    {
        _animationTrigger = transform.parent.parent.GetComponent<AnimationTrigger>();
    }

    public IEnumerator Animate(AnimatableEntity subject, float speed)
    {
        if (GoToNextCodingBlock)
            _animationTrigger.ProgressCodingBlocksAnimation(CodingBlockSize);

        if (WaypointToWaitOn != null) 
            while (!WaypointToWaitOn.IsDone) yield return null;

        yield return AnimateInstance(subject, speed);
    }

    protected abstract IEnumerator AnimateInstance(AnimatableEntity subject, float speed);

    public void DrawGizmos(Transform prevWaypoint)
    {
        if (WaypointToWaitOn)
            GizmosArrow.Draw(WaypointToWaitOn.transform.position, transform.position, Color.black);
        DrawGizmosInstance(prevWaypoint);
    }
    protected abstract void DrawGizmosInstance(Transform prevWaypoint);

    private void OnValidate()
    {
        if (CodingBlockSize < 1)
            CodingBlockSize = 1;
    }
}
