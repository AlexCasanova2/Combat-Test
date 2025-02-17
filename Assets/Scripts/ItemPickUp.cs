﻿using UnityEngine;

public class ItemPickUp : Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        
        //Debug.Log("Picking up: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
            Hide();
        }

        
    }
    public void Hide()
    {
        if (Input.GetButtonDown("Inventory") )
        {
            Debug.Log("Lo desactivo");
            tutorialUI.SetActive(false);
        }
    }


}
