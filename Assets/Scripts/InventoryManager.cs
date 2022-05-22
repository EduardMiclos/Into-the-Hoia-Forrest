using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    private InventoryManager() {}


    [SerializeField]
    private Animator animator;

    internal bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    internal void OpenInventory()
    {
        animator.SetBool("isOpen", true);
        isOpen = true;
    }

    internal void CloseInventory()
    {
        animator.SetBool("isOpen", false);
        isOpen = false;
    }
}
