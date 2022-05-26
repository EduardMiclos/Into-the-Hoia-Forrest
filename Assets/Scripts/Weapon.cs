using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    [SerializeField]
    private Sprite[] weaponSprites;

    public ItemType typeOfItem {get; set;}
    public SpriteRenderer sprite {get; set;}
    public int amount {get; set;}


    /* We play this audio source whenever the weapon is upgraded. */
    private AudioSource audioSource;

    /* This is the weapon level. It will help us
    access different sprites from the weaponSprites array. */
    private int weaponSkill;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

        typeOfItem = ItemType.Weapon;
        amount = 1;

        /* Initially, the player has no weapons. */        
        weaponSkill = -1;
    }

    private void PlayUpgradeSound()
    {
        audioSource.Play();
    }

    internal void Upgrade()
    {
        weaponSkill++;
        sprite.sprite = weaponSprites[weaponSkill];

        PlayUpgradeSound();
    }

    public void increaseAmount()
    {
        amount++;
    }
    public void decreaseAmount()
    {
        amount--;
    }
}
