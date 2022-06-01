using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameManager() { }

    public Speed initialPlayerSpeed = new Speed(4f, 4f);
    private Vector3 onSceneStartPlayerPosition = new Vector3(-7.39f, 0.06f, 0f);

    [SerializeField]
    private Player player;

    [SerializeField]
    private EventAdapter eventAdapter;

    [SerializeField]
    private Teleport teleport;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        eventAdapter.SetButtonsListeners(
            playerInventory: InventoryManager.instance.playerInventory,
            BackpackItems: InventoryManager.instance.UIInventory.Items,
            PrimaryItems: InventoryManager.instance.UIInventory.PrimaryItems,
            PrimaryWeapon: InventoryManager.instance.UIInventory.PrimaryWeapon);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = onSceneStartPlayerPosition;
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

    public void ActivateTeleport()
    {
        teleport.isActive = true;
    }
}
