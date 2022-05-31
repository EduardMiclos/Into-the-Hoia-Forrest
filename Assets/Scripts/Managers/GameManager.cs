using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameManager() { }

    public Speed initialPlayerSpeed = new Speed(4f, 4f);

    [SerializeField]
    private EventAdapter eventAdapter;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        eventAdapter.SetButtonsListeners(
            playerInventory: InventoryManager.instance.playerInventory,
            BackpackItems: InventoryManager.instance.UIInventory.Items,
            PrimaryItems: InventoryManager.instance.UIInventory.PrimaryItems,
            PrimaryWeapon: InventoryManager.instance.UIInventory.PrimaryWeapon);
    }

    public GameManager GetInstance()
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
        if (eventAdapter.IsKeyPressed(KeyCode.I))
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

    public static GameObject GetObjectChild(GameObject obj, string childName)
    {
        Transform childTransform = obj.transform.Find(childName);

        if (childTransform == null)
        {
            return null;
        }

        return childTransform.gameObject;
    }
}
