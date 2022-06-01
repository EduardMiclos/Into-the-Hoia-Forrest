using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public bool isActive = false;
    public GameObject currentAttachmentObject;
    public int currentSubinventoryType, currentSlotIndex;


    void Start()
    {
        SetActive(false);
    }

    void Update() {}

    private void SetPosition(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y, 0f);
    }

    public void SetActive(bool activeValue)
    {
        isActive = activeValue;
        gameObject.SetActive(activeValue);
    }

    public InventoryMenu AttachToGameObject(GameObject obj, int subinventoryType, int slotIndex)
    {
        currentAttachmentObject = obj;
        currentSubinventoryType = subinventoryType;
        currentSlotIndex = slotIndex;

        SetPosition(obj.transform.position.x + 300, obj.transform.position.y);

        return this;
    }


    public void BtnMoveToF_OnPress()
    {
        //InventoryManager.instance.MoveItemToF(currentAttachmentObject, currentSubinventoryType, currentSlotIndex);
    }

    public void BtnMoveToG_OnPress()
    {
        //InventoryManager.instance.MoveItemToG(currentAttachmentObject, currentSubinventoryType, currentSlotIndex);
    }

    public void BtnDropUnit_OnPress()
    {
        //InventoryManager.instance.DropItemUnit(currentAttachmentObject, currentSubinventoryType, currentSlotIndex);
    }

    public void BtnDropAll_OnPress()
    {
        //InventoryManager.instance.DropItem(currentAttachmentObject, currentSubinventoryType, currentSlotIndex);
    }

    public void BtnSetPrimaryWeapon_OnPress()
    {
        //InventoryManager.instance.MoveToPrimaryWeapon(currentAttachmentObject, currentSubinventoryType, currentSlotIndex);
    }
}
