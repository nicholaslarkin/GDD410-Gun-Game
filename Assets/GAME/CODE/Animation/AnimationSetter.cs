using UnityEngine;

/// <summary>
/// Used for animation events in timeline (sets bools off so the animator can transition properly)
/// Non-modular at the moment as it only pertains to the player and handles specific names
/// </summary>

public class AnimationSetter : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void OnAnimationEnter()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isReloading", false);
    }

    public void OnAnimationExit()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isReloading", false);
    }
}
