using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public static InventoryManager instance = null;
    private InventoryManager() {}
    
    internal bool isInventoryOpen;

    void Awake()
    {
        isInventoryOpen = false;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    internal void OpenInventory()
    {
        animator.SetBool("isOpen", true);
        isInventoryOpen = true;
    }

    internal void CloseInventory()
    {
        animator.SetBool("isOpen", false);
        isInventoryOpen = false;
    }
}
