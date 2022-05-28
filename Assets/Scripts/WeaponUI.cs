using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; set;}

    /* We play this audio source whenever the weapon is upgraded. */
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayUpgradeSound()
    {
        audioSource.Play();
    }
}
