using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    private Animator animator;
    public GameObject Items { get; set; }
    public GameObject PrimaryItems { get; set; }
    public GameObject PrimaryWeapon { get; set; }
    
    public GameObject inventoryItemPrefab;

    public static Color inactiveSlotColor = new Color(1f, 1f, 1f, 0.78f);
    public static Color activeSlotColor = new Color(0.75f, 1f, 0.94f, 0.78f);

    public static Color activeButtonColor = new Color(0.3481132f, 0.3646901f, 0.3867925f, 0.6901961f);
    public static Color inactiveButtonColor = new Color(0.4433962f, 0.106972f, 0.08114985f, 0.7568628f);

    void Awake()
    {
        animator = GetComponent<Animator>();

        Items = GameManager.GetObjectChild(this.gameObject, "Items");
        PrimaryItems = GameManager.GetObjectChild(this.gameObject, "Primary Items");
        PrimaryWeapon = GameManager.GetObjectChild(GameManager.GetObjectChild(this.gameObject, "Primary Weapon"), "Weapon");
    }

    public void OpenAnimation()
    {
        animator.SetBool("isOpen", true);
    }
    
    public void CloseAnimation()
    {
        animator.SetBool("isOpen", false);
    }

    private GameObject GetInventoryPrefabOfObject(GameObject obj)
    {
        return GameManager.GetObjectChild(obj, "InventoryItem(Clone)");
    }

    private GameObject SetInventoryItemToObject(GameObject obj)
    {
        return SetInventoryItemToObject(obj, inventoryItemPrefab);
    }

    private GameObject SetInventoryItemToObject(GameObject obj, GameObject inventoryItem)
    {
        GameObject objItem = Instantiate(inventoryItem, obj.transform);
        objItem.name = "InventoryItem(Clone)";

        GameManager.GetObjectChild(objItem, "Image").GetComponent<Image>().enabled = true;
        GameManager.GetObjectChild(objItem, "Amount").GetComponent<Text>().enabled = true;

        objItem.transform.SetSiblingIndex(0);
        return objItem;
    }

    public bool AddItem(GameObject itemSlot, Item item)
    {
        if (itemSlot == null)
        {
            return false;
        }

        GameObject inventoryItem = SetInventoryItemToObject(itemSlot);
        ConfigureInventoryItem(inventoryItem, item.sprite, item.amount);

        return true;
    }

    public bool AddBackpackItem(Item item, int listIndex)
    {
        GameObject itemSlot = GameManager.GetObjectChild(Items, listIndex.ToString());
        return AddItem(itemSlot, item);
    }

    public bool RemoveInventoryItem(GameObject itemSlot)
    {
        GameObject.Destroy(GetInventoryPrefabOfObject(itemSlot));
        return true;
    }

    public bool AddPrimaryItem(Item item, int listIndex)
    {
        GameObject itemSlot = GameManager.GetObjectChild(PrimaryItems, listIndex.ToString());
        return AddItem(itemSlot, item);
    }


    public void AddPrimaryWeapon(Weapon weapon)
    {
        GameObject inventoryItem = SetInventoryItemToObject(PrimaryWeapon);
        ConfigureInventoryItem(inventoryItem, weapon.sprite, weapon.amount);
    }
    
    private void SetItemAmount(GameObject obj, int amount)
    {
        GameObject Amount = GameManager.GetObjectChild(obj, "Amount");

        string whiteSpace = " ";
        if (amount <= 9)
        {
            whiteSpace += " ";
        }

        Amount.GetComponent<Text>().text = whiteSpace + amount.ToString();
    }

    public void SetChildItemAmount(GameObject obj, int amount)
    {
        GameObject inventoryItem = GetInventoryPrefabOfObject(obj);
        SetItemAmount(inventoryItem, amount);
    }

    public void SetPrimaryWeaponAmount(int amount)
    {
        SetChildItemAmount(PrimaryWeapon, amount);
    }

    public void SetBackpackItemAmount(int amount, int itemIndex)
    {
        GameObject inventorySlot = GameManager.GetObjectChild(Items, itemIndex.ToString());
        GameObject inventoryItem = GetInventoryPrefabOfObject(inventorySlot);
        SetItemAmount(inventoryItem, amount);
    }

    public void SetPrimaryItemAmount(int amount, int itemIndex)
    {
        GameObject inventorySlot = GameManager.GetObjectChild(PrimaryItems, itemIndex.ToString());
        GameObject inventoryItem = GetInventoryPrefabOfObject(inventorySlot);
        SetItemAmount(inventoryItem, amount);
    }

    private void ConfigureInventoryItem(GameObject inventoryItem, Sprite sprite, int amount)
    {
        GameObject image = GameManager.GetObjectChild(inventoryItem, "Image");
        image.GetComponent<Image>().sprite = sprite;
        image.GetComponent<Image>().SetNativeSize();

        SetItemAmount(inventoryItem, amount);

        inventoryItem.transform.SetSiblingIndex(0);
    }

    public void DisplayActiveSlot(GameObject itemSlot, bool activeValue)
    {
        if (activeValue == true)
        {
            itemSlot.GetComponent<Image>().color = InventoryUI.activeSlotColor;
        }
        else
        {
            itemSlot.GetComponent<Image>().color = InventoryUI.inactiveSlotColor;
        }
    }

    public void SetWeaponSelectButtonActive(GameObject obj, bool activeValue)
    {
        GameObject button = GameManager.GetObjectChild(obj, "BtnSetPrimaryWeapon");
        button.GetComponent<Button>().enabled = activeValue;
        
        if (activeValue == true)
        {
            button.GetComponent<Image>().color = InventoryUI.activeButtonColor;
        }
        else
        {
            button.GetComponent<Image>().color = InventoryUI.inactiveButtonColor;
        }
    }

    public void InterchangeItems(InventoryType sourceInventory, GameObject itemSlot1, GameObject itemSlot2)
    {
        if (itemSlot1 == itemSlot2)
        {
            return;
        }

        GameObject inventoryItem1 = GetInventoryPrefabOfObject(itemSlot1);
        GameObject inventoryItem2 = GetInventoryPrefabOfObject(itemSlot2);

        RemoveInventoryItem(itemSlot1);
        RemoveInventoryItem(itemSlot2);

        if (inventoryItem2 != null)
        {
            SetInventoryItemToObject(itemSlot1, inventoryItem2);
        }
        else
        {
            if (sourceInventory == InventoryType.PrimaryWeapon)
            {
                InventoryManager.instance.visualWeaponObject.SetSprite(null);
            }
        }

        if (inventoryItem1 != null)
        {
            SetInventoryItemToObject(itemSlot2, inventoryItem1);
        }
    }
}