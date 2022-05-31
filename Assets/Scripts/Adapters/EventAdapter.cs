using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventAdapter : MonoBehaviour
{
    public EventAdapter() { }

    public void SetButtonsListeners(Inventory playerInventory, GameObject BackpackItems, GameObject PrimaryItems, GameObject PrimaryWeapon)
    {

        for (int i = 0; i < playerInventory.backpackCapacity; i++)
        {
            int x = i;
            GameObject itemSlot = GameManager.GetObjectChild(BackpackItems, i.ToString());
            GameObject button = GameManager.GetObjectChild(itemSlot, "Button");

            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(0, x); });
        }

        for (int i = 0; i < playerInventory.primaryCapacity; i++)
        {
            int x = i;
            GameObject itemSlot = GameManager.GetObjectChild(PrimaryItems, i.ToString());
            GameObject button = GameManager.GetObjectChild(itemSlot, "Button");

            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(1, x); });
            
        }

        {
            GameObject button = GameManager.GetObjectChild(PrimaryWeapon, "Button");
            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(2, 0); });
        }
    }

    public bool IsKeyPressed(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool ButtonPressed(int inventoryType, int slotIndex)
    {
        GameObject pressedObject = null;
        
        switch (inventoryType)
        {
            /* Backpack. */
            case 0:
               pressedObject = GameManager.GetObjectChild (
                   obj: InventoryManager.instance.UIInventory.Items, 
                   childName: slotIndex.ToString());
                break;
            /* Primary items. */
            case 1:
                pressedObject = GameManager.GetObjectChild(
                   obj: InventoryManager.instance.UIInventory.PrimaryItems,
                   childName: slotIndex.ToString());
                break;
            /* Primary Weapon. */
            case 2:
                pressedObject = InventoryManager.instance.UIInventory.PrimaryWeapon;
                break;
            default:
                return false;
        }



        if (InventoryManager.instance.inventoryMenu.isActive == true &&
            InventoryManager.instance.inventoryMenu.currentAttachmentObject == pressedObject)
        {
            InventoryManager.instance.inventoryMenu.SetActive(false);
        }
        else
        {
            InventoryManager.instance.inventoryMenu.SetActive(true).AttachedToGameObject(pressedObject);
        }

        return true;
    }
}
