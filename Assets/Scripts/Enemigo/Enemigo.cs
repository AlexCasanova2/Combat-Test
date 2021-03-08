using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemigo : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        //Attack the enemy
        CharacterCombat playercombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playercombat != null)
        {
            playercombat.Attack(myStats);
        }
    }
}
