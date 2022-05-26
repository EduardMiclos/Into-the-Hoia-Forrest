using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameManager() {}

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
            if (InventoryManager.instance.isInventoryOpen)
            {
                InventoryManager.instance.CloseInventory();
            }
            else
            {
                InventoryManager.instance.OpenInventory();
            }
        }
    }
}
