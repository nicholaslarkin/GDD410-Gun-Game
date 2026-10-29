using UnityEngine;

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
