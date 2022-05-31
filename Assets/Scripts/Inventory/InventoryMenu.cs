using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public bool isActive = false;
    public GameObject currentAttachmentObject;

    void Start()
    {
        SetActive(false);
    }

    void Update()
    {
    }

    private void SetPosition(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y, 0f);
    }

    public InventoryMenu SetActive(bool activeValue)
    {
        isActive = activeValue;
        gameObject.SetActive(activeValue);
        return this;
    }

    public void AttachedToGameObject(GameObject obj)
    {
        currentAttachmentObject = obj;
        SetPosition(obj.transform.position.x + 300, obj.transform.position.y + 20);
    }
}
