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

            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(InventoryType.Backpack, x); });
        }

        for (int i = 0; i < playerInventory.primaryCapacity; i++)
        {
            int x = i;
            GameObject itemSlot = GameManager.GetObjectChild(PrimaryItems, i.ToString());
            GameObject button = GameManager.GetObjectChild(itemSlot, "Button");

            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(InventoryType.Primary, x); });
            
        }

        {
            GameObject button = GameManager.GetObjectChild(PrimaryWeapon, "Button");
            button.GetComponent<Button>().onClick.AddListener(delegate { ButtonPressed(InventoryType.PrimaryWeapon, 0); });
        }
    }

    public bool IsKeyPressed(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool ButtonPressed(InventoryType inventoryType, int slotIndex)
    {
        GameObject pressedObject = null;
        Item selectedItem = null;
        bool isPrimaryWeapon = false;

        switch (inventoryType)
        {
            /* Backpack items. */
            case InventoryType.Backpack:
                selectedItem = InventoryManager.instance.playerInventory.backpackItems[slotIndex];
                if (selectedItem == null)
                {
                    return false;
                }
                isPrimaryWeapon = false;
                pressedObject = GameManager.GetObjectChild(
                    obj: InventoryManager.instance.UIInventory.Items,
                    childName: slotIndex.ToString());
                break;

            /* Primary items. */
            case InventoryType.Primary:
                selectedItem = InventoryManager.instance.playerInventory.primaryItems[slotIndex];
                if (selectedItem == null)
                {
                    return false;
                }

                isPrimaryWeapon = false;
                pressedObject = GameManager.GetObjectChild(
                   obj: InventoryManager.instance.UIInventory.PrimaryItems,
                   childName: slotIndex.ToString());
                break;

            /* Primary Weapon. */
            case InventoryType.PrimaryWeapon:
                selectedItem = InventoryManager.instance.playerInventory.primaryWeapon;
                if (selectedItem == null)
                {
                    return false;
                }
                pressedObject = InventoryManager.instance.UIInventory.PrimaryWeapon;
                isPrimaryWeapon = true;
                break;
            default:
                return false;
        }

        if (selectedItem.typeOfItem != ItemType.Weapon || isPrimaryWeapon == true)
        {
            InventoryManager.instance.UIInventory.SetWeaponSelectButtonActive(InventoryManager.instance.inventoryMenu.gameObject, false);
        }
        else
        {
            InventoryManager.instance.UIInventory.SetWeaponSelectButtonActive(InventoryManager.instance.inventoryMenu.gameObject, true);
        }


        if (InventoryManager.instance.inventoryMenu.isActive == true &&
            InventoryManager.instance.inventoryMenu.currentAttachmentObject == pressedObject)
        {
            InventoryManager.instance.inventoryMenu.SetActive(false);
            InventoryManager.instance.UIInventory.DisplayActiveSlot(pressedObject, false);
        }
        else
        {
            if (InventoryManager.instance.inventoryMenu.currentAttachmentObject != null)
            {
                InventoryManager.instance.UIInventory.DisplayActiveSlot(InventoryManager.instance.inventoryMenu.currentAttachmentObject, false);
            }

            InventoryManager.instance.inventoryMenu.AttachToGameObject(pressedObject, inventoryType, slotIndex).SetActive(true);
            InventoryManager.instance.UIInventory.DisplayActiveSlot(pressedObject, true);
        }

        return true;
    }
}
