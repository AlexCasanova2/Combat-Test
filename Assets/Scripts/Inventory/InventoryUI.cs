using Cinemachine;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public CinemachineFreeLook freelook;
    bool _pressed;

    public static bool inventoryPressed = false; 

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        _pressed = GameController.escapePressed;

        if (Input.GetButtonDown("Inventory") && !_pressed)
        {
            freelook.enabled = !freelook.enabled;
            inventoryUI.gameObject.SetActive(!inventoryUI.activeSelf);

            if (GameController.controlHUD)
            {
                GameController.PauseYMouseControl(false, false);
                //Control movimiento personaje
                FindObjectOfType<ThirdPersonCharacter>().enabled = true;
                FindObjectOfType<ThirdPersonUserControl>().enabled = true;
                inventoryPressed = false;
            }
            else
            {
                GameController.PauseYMouseControl(false, true);
                //Control movimiento personaje
                FindObjectOfType<ThirdPersonCharacter>().enabled = false;
                FindObjectOfType<ThirdPersonUserControl>().enabled = false;
                inventoryPressed = true;
            }
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


