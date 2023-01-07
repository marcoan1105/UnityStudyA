using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsController))]
public class CharacterCombatController : MonoBehaviour
{
    CharacterStatsController myStats;
    CharacterStatsController opponentStats;

    [SerializeField]
    float attackSpeed = 1f;

    [SerializeField]
    float attackDelay = .6f;

    const float combatCooldown = 5;
    float lastAttackTime;

    private float attackCooldown = 0f;

    public bool InCombat { get; set; }

    public event System.Action OnAttack;

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    void Start()
    {
        myStats = GetComponent<CharacterStatsController>();
    }

    public void Attack(CharacterStatsController targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;
            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;            
        }        
    }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.Damage.GetValue());

        if (opponentStats.CurrentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
