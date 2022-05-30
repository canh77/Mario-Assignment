using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartBoudary : MonoBehaviour
{
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag != "Player0")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),
                gameObject.GetComponent<Collider2D>());
        }
    }
}
