using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsController))]
public class Enemy : InteractableController
{

    PlayerManager playerManager;

    CharacterStatsController myStats;

    void Start()
    {
        base.Start();
        playerManager = PlayerManager.Instance;
        myStats = GetComponent<CharacterStatsController>();
    }

    public override void Interact()
    {
        base.Interact();

        CharacterCombatController playerCombat = playerManager.Player.GetComponent<CharacterCombatController>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
