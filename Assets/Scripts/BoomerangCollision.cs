using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Target" tag
        if (collision.gameObject.CompareTag("Target"))
        {
            // Destroy the collided object
            Destroy(collision.gameObject);
        }
    }
}
