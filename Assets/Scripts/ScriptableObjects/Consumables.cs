using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumables")]
public class Consumables : Item
{
    public int healingPoints;

    public override void Use()
    {
        base.Use();
        //Eliminarlo del inventario
        RemoveFromInventory();
    }
}
