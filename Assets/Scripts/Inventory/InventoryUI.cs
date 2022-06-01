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

    public bool AddItem(GameObject itemSlot, Item item)
    {
        if (itemSlot == null)
        {
            return false;
        }

        GameObject inventoryItem = Instantiate(inventoryItemPrefab, itemSlot.transform);
        ConfigureInventoryItem(inventoryItem, item.sprite, item.amount);

        return true;
    }

    public bool AddBackpackItem(Item item, int listIndex)
    {
        GameObject itemSlot = GameManager.GetObjectChild(Items, listIndex.ToString());
        return AddItem(itemSlot, item);
    }

    public bool AddPrimaryItem(Item item, int listIndex)
    {
        GameObject itemSlot = GameManager.GetObjectChild(PrimaryItems, listIndex.ToString());
        return AddItem(itemSlot, item);
    }


    public void AddPrimaryWeapon(Weapon weapon)
    {
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, PrimaryWeapon.transform);
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

    public void SetPrimaryWeaponAmount(int amount)
    {
        GameObject inventoryItem = GameManager.GetObjectChild(PrimaryWeapon, "InventoryItem(Clone)");
        SetItemAmount(inventoryItem, amount);
    }

    public void SetBackpackItemAmount(int amount, int itemIndex)
    {
        GameObject inventorySlot = GameManager.GetObjectChild(Items, itemIndex.ToString());
        GameObject inventoryItem = GameManager.GetObjectChild(inventorySlot, "InventoryItem(Clone)");
        SetItemAmount(inventoryItem, amount);
    }

    public void SetPrimaryItemAmount(int amount, int itemIndex)
    {
        GameObject inventorySlot = GameManager.GetObjectChild(PrimaryItems, itemIndex.ToString());
        GameObject inventoryItem = GameManager.GetObjectChild(inventorySlot, "InventoryItem(Clone)");
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
}
