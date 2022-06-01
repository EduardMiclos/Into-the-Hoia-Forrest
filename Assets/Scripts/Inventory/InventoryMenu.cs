using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public bool isActive = false;
    public GameObject currentAttachmentObject;
    public InventoryType currentInventoryType;
    public int currentSlotIndex;

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

    public InventoryMenu AttachToGameObject(GameObject obj, InventoryType inventoryType, int slotIndex)
    {
        currentAttachmentObject = obj;
        currentInventoryType = inventoryType;
        currentSlotIndex = slotIndex;

        SetPosition(obj.transform.position.x + 300, obj.transform.position.y);

        return this;
    }

    public void BtnMoveToF_OnPress()
    {
        InventoryManager.instance.MoveItemToF(currentAttachmentObject, currentInventoryType, currentSlotIndex);
        SetActive(false);
    }

    public void BtnMoveToG_OnPress()
    {
        InventoryManager.instance.MoveItemToG(currentAttachmentObject, currentInventoryType, currentSlotIndex);
        SetActive(false);
    }

    public void BtnDropUnit_OnPress()
    {
        InventoryManager.instance.DropItemUnit(currentAttachmentObject, currentInventoryType, currentSlotIndex);
        SetActive(false);
    }

    public void BtnDropAll_OnPress()
    {
        InventoryManager.instance.DropItem(currentAttachmentObject, currentInventoryType, currentSlotIndex);
        SetActive(false);
    }

    public void BtnSetPrimaryWeapon_OnPress()
    {
        InventoryManager.instance.MoveToPrimaryWeapon(currentAttachmentObject, currentInventoryType, currentSlotIndex);
    }
}
