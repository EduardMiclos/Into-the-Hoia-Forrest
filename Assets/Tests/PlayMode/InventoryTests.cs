using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    [SetUp]
    public void Setup()
    {
        //GameObject gameGameObject =
          //  MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
    }


    [UnityTest]
    public IEnumerator GetInventoryManager_OneCall_True()
    {
        InventoryManager instance = InventoryManager.instance;

        yield return new WaitForSeconds(0.1f);

        Assert.IsNotNull(instance);
    }

    [UnityTest]
    public IEnumerator AddWeapon_OneWeapon_True()
    {
        GameObject obj = new GameObject();
        
        obj.AddComponent<InventoryManager>();
        obj.AddComponent<Inventory>();

        obj.GetComponent<InventoryManager>().playerInventory = obj.GetComponent<Inventory>();
        InventoryManager instance = obj.GetComponent<InventoryManager>();
       

        yield return new WaitForSeconds(0.1f);

        Assert.IsNotNull(instance.playerInventory);
    }
}
