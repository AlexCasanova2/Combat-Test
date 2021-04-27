using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    GameObject playerPrefab;
    
    public virtual void Use()
    {
        //Usar el item del inventario
        if (name == "Health Potion")
        {
            Debug.Log("Using: " + name);
            playerPrefab =  GameObject.FindWithTag("Player");
            playerPrefab.GetComponentInChildren<CharacterStats>().HealPlayer(5);
        }
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
