using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Hay mas de una instancia del inventario");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject inventario;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    private void Start()
    {
        inventario.SetActive(false);
    }
    public bool Add (Item item) 
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("No tienes más espacio en el inventario");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
        
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
