using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /* We play this audio source whenever the weapon is upgraded. */
    private AudioSource audioSource;

    /* Current sprite. */
    private SpriteRenderer sprite;

     [SerializeField]
    private Sprite[] weaponSprites;

    /* This is the weapon level. It will help us
    access different sprites from the weaponSprites array. */
    private int weaponSkill;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

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
}
