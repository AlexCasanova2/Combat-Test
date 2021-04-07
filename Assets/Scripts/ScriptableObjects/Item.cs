using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    
    
    public virtual void Use()
    {
        

        //Usar el item del inventario

        if (name == "Health Potion")
        {
            Debug.Log("Using: " + name);
            //playerPrefab.GetComponentInChildren<CharacterStats>().HealPlayer(5);
        }
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
