using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventReceiver : MonoBehaviour
{
    public CharacterCombatController combat;

    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
