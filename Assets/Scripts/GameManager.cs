using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameManager() {}


    [SerializeField]
    private InventoryManager inventoryManager;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryManager.isOpen)
            {
                inventoryManager.CloseInventory();
            }
            else
            {
                inventoryManager.OpenInventory();
            }
        }
    }
}
