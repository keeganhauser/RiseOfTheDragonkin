using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : AnimationController
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization += CombatStart;
        GameEventsManager.Instance.EnemyEvents.onEnemyDecideAttack += TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack += TriggerHitAnim;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CombatEvents.onCombatPostInitialization -= CombatStart;
        GameEventsManager.Instance.EnemyEvents.onEnemyDecideAttack -= TriggerAttackAnim;
        GameEventsManager.Instance.PlayerEvents.onPlayerDecideAttack -= TriggerHitAnim;
    }



    private void TriggerHitAnim()
    {
        StartCoroutine(TriggerAnimation("Hit"));
    }
}
