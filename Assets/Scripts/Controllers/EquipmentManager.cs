using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    public bool isEquipped;
    public bool haveEquip;

    public Animator anim;
    public ItemPickUp basicHelmet;
    public ItemPickUp basicSword;
    public ItemPickUp basicShield;

    [Header("ShowTutorial")]
    public GameObject uiTutorial;
    public TextMeshProUGUI textTutorial;
    int times;

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots =  System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && isEquipped || Input.GetKeyDown(KeyCode.U) && haveEquip)
        {
            UnequipAll();
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
        anim.SetBool("isEquipped", isEquipped);
    }
    
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        //Debug.Log("slotIndex: " + slotIndex);

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] !=null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        //Debug.Log("Equipando: " + newItem.name);
        //Debug.Log(newItem.GetType());
        
        //Debug.Log(currentEquipment[slotIndex]);
        if (slotIndex == 0)
        {
            haveEquip = true;
            basicHelmet.Activar();
            //Debug.Log("Estas equipando un casco");
        }
        if (slotIndex == 3)
        {
            basicSword.Activar();
            times++;
            if (times == 1)
            {
                uiTutorial.SetActive(true);
                textTutorial.SetText("Press 'U' to unequip");
               
            }
            if (Input.GetKey(KeyCode.U))
            {
                uiTutorial.SetActive(false);
            }
            //Al equipar un arma cambiamos el valor del bool a true para poder habilitar la animación de atacar
            isEquipped = true;
        }
        if (slotIndex == 4)
        {
            haveEquip = true;
            basicShield.Activar();
            //Debug.Log("Estas equipando un escudo");
        }

    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            isEquipped = false;
            haveEquip = false;
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            basicHelmet.Desactivar();
            basicSword.Desactivar();
            basicShield.Desactivar();
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    

}
