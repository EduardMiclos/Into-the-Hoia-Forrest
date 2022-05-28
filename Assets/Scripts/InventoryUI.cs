using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    private Animator animator;

    private GameObject Items;
    private GameObject PrimaryItems;
    private GameObject PrimaryWeapon;

    public GameObject inventoryItemPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();

        Items = getChild(this.gameObject, "Items");
        PrimaryItems = getChild(this.gameObject, "Primary Items");
        PrimaryWeapon = getChild(getChild(this.gameObject, "Primary Weapon"), "Weapon");
    }

    public void OpenAnimation()
    {
        animator.SetBool("isOpen", true);
    }
    
    public void CloseAnimation()
    {
        animator.SetBool("isOpen", false);
    }

    public bool AddBackpackItem(Item item, int listIndex)
    {
        GameObject itemSlot = getChild(Items, listIndex.ToString());

        if (itemSlot == null)
        {
            return false;
        }

        GameObject inventoryItem = Instantiate(inventoryItemPrefab, itemSlot.transform);
        ConfigureInventoryItem(inventoryItem, item.sprite, item.amount + 10);

        return true;
    }

    public void AddPrimaryWeapon(Weapon weapon)
    {
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, PrimaryWeapon.transform);
        ConfigureInventoryItem(inventoryItem, weapon.sprite, weapon.amount);
    }

    private GameObject getChild(GameObject obj, string childName)
    {
        Transform childTransform = obj.transform.Find(childName);

        if (childTransform == null)
        {
            return null;
        }

        return childTransform.gameObject;
    }

    private void ConfigureInventoryItem(GameObject inventoryItem, Sprite sprite, int amount)
    {
        GameObject Image = getChild(inventoryItem, "Image");

        Image.GetComponent<Image>().sprite = sprite;

        GameObject Amount = getChild(inventoryItem, "Amount");

        string whiteSpace = " ";
        if (amount <= 9)
        {
            whiteSpace += " ";
        }

        Amount.GetComponent<Text>().text = whiteSpace + amount.ToString();
    }
}
