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
        //Debug.Log("Using: " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void AddToEquipmentList()
    {
        //Añade este item a la lista de objetos equipados
    }
}
