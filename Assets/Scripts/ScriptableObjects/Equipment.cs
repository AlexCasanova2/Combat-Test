using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Equipment" , menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    //public SkinnedMeshRenderer mesh;
    //public GameObject equiparObjeto;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        //Equipar item
        EquipmentManager.instance.Equip(this);
        //Eliminarlo del inventario
        RemoveFromInventory();
    }

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}