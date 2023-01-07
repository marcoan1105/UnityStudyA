using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip ReplaceableAttackAnim;
    public  AnimationClip[] DefaultAttackAnimSet;
    protected AnimationClip[] CurrentAttackAnimSet;

    const float locomationAnimationSmoothTime = .1f;
    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombatController combat;

    public AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponentInChildren<CharacterCombatController>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        
        animator.runtimeAnimatorController = overrideController;

        CurrentAttackAnimSet = DefaultAttackAnimSet;

        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("InCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, CurrentAttackAnimSet.Length);

        overrideController[ReplaceableAttackAnim.name] = CurrentAttackAnimSet[attackIndex];
    }
}
