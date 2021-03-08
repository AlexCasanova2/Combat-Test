using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public ItemPickUp basicHelmet;
    public ItemPickUp basicSword;
    public ItemPickUp basicShield;

    //public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    // SkinnedMeshRenderer[] currentMeshes;

   

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots =  System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //currentMeshes = new SkinnedMeshRenderer[numSlots];
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
        //SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        //newMesh.transform.parent = targetMesh.transform;

        //Debug.Log("Equipando" + newItem.name);
        //Debug.Log(newItem.GetType());
        
        Debug.Log(currentEquipment[slotIndex]);
        if (slotIndex == 0)
        {
            basicHelmet.Activar();
            //Debug.Log("Estas equipando un casco");

            if (Input.GetMouseButton(0))
            {
                
            }
        }
        if (slotIndex == 3)
        {
            basicSword.Activar();
            //Debug.Log("Estas equipando un arma");
            //Al equipar un arma cambiamos el valor del bool a true para poder habilitar la animación de atacar
            isEquipped = true;
        }
        if (slotIndex == 4)
        {
            basicShield.Activar();
            //Debug.Log("Estas equipando un escudo");
        }

        /*newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;*/

    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            /*if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }*/
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
            AudioSource audio = GetComponent<AudioSource>();

            audio.Play();
        }
        
    }

}
