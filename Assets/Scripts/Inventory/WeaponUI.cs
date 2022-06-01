using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; set;}

    /* We play this audio source whenever the weapon is upgraded. */
    private AudioSource audioSourceUpgrade;

    /* We play this audio source whenever the weapon is dropped. */
    private AudioSource audioSourceDrop;

    void Awake()
    {
        audioSourceUpgrade = GetComponents<AudioSource>()[0];
        audioSourceDrop = GetComponents<AudioSource>()[1];

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayUpgradeSound()
    {
        audioSourceUpgrade.Play();
    }

    public void PlayDropSound()
    {
        audioSourceDrop.Play();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
