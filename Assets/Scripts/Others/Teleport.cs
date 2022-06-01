using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : Collidable
{

    public bool isActive { get; set; } = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (isActive && coll.name.Equals("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
