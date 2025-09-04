using UnityEngine;

public class CharacterAnimator
{
    private readonly Animator animator;

    public const string WALK = "Walk";
    public const string FIRE = "Fire";
    public const string BURST_FIRE = "BurstFire";
    private const string HIT = "Hit";
    public const string DEATH = "Death";
    public const string DEFAULT = "Default";
    public const string RUN = "Run";

    private bool done = true;

    public CharacterAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void DisableAnimations()
    {
        animator.SetBool(WALK, false);
        animator.SetBool(RUN, false);
        animator.SetBool(FIRE, false);
        animator.SetBool(BURST_FIRE, false);
        animator.SetBool(HIT, false);
    }

    public void DisableAnimationsWithAnimator()
    {
        DisableAnimations();
        animator.enabled = false;
    }

    public void StartWalkAnimation()
    {
        animator.SetBool(WALK, true);
    }

    public bool IsFireEnd()
    {
        return !animator.GetBool(FIRE);
    }

    public bool IsBurstFireEnd()
    {
        return !animator.GetBool(BURST_FIRE);
    }

    public bool IsInTransition() => animator.IsInTransition(0);

    public void StartHitAnimation()
    {
        if (!done)
        {
            animator.SetBool(HIT, true);
        }
    }

    public void StopHitAnimation()
    {
        if (!done)
        {
            animator.SetBool(HIT, false);
        }
    }

    public bool IsHitEnd()
    {
        return !animator.GetBool(HIT);
    }

    public void ToggleHitAnimation()
    {
        this.done = !done;
    }

    public void SetAnimation(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void ApplyRootMotion(bool apply)
    {
        animator.applyRootMotion = apply;
    }
   
}
