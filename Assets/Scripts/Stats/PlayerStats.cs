using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        
    }

    //Cada vez que se equipa un elemento se llama a esta funcion y se le aplican los modificadores correspondientes
    void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
    }


}
