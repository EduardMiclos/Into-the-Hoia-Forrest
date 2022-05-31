using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    public static ItemSprites instance = null;

    public Sprite[] weapons;

    public Sprite coin;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
