using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameManager() {}
    public Speed initialPlayerSpeed = new Speed(4f, 4f);

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameManager getInstance()
    {
        if (instance == null)
        {
            GameObject gameObject = new GameObject();
            instance = gameObject.AddComponent<GameManager>();
        }
        return instance;
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
