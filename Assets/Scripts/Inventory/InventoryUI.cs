using Cinemachine;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public CinemachineFreeLook freelook;
    bool ok;
    //Cursor lock
    bool cursorLockedVar;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cursorLockedVar = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !cursorLockedVar)
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.activeSelf);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLockedVar = true;
            //Debug.Log(cursorLockedVar);
            freelook.enabled = true;
        }
        else if (Input.GetButtonDown("Inventory") && cursorLockedVar)
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.activeSelf);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLockedVar = false;
            //Debug.Log(cursorLockedVar);
            freelook.enabled = false;
            //ok = !ok;
        }
    }

    void UpdateUI(){
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}


