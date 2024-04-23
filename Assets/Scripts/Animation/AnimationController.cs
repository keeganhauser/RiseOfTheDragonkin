using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    [SerializeField] protected Direction combatDirection;
    protected Animator animator;
    protected Rigidbody2D rb2d;

    private void Update()
    {
        UpdateAnimations();
    }

    protected void TriggerAttackAnim()
    {
        StartCoroutine(TriggerAnimation("Attack"));
    }

    protected void CombatStart()
    {
        SetAnimator(combatDirection);
    }

    private void UpdateAnimations()
    {
        if (rb2d == null || animator == null) return;
        // Update animator parameters
        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            animator.SetFloat("X", rb2d.velocity.x);
            animator.SetFloat("Y", rb2d.velocity.y);

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public IEnumerator TriggerAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
        yield return new WaitForFixedUpdate();
        animator.ResetTrigger(triggerName);
    }

    protected virtual void SetAnimator(Direction direction)
    {
        float x = 0f;
        float y = 0f;

        switch (direction)
        {
            case Direction.Up:
                y = 1f;
                break;
            case Direction.Down:
                y = -1f;
                break;
            case Direction.Left:
                x = -1f;
                break;
            case Direction.Right:
                x = 1f;
                break;
            default:
                break;
        }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}
