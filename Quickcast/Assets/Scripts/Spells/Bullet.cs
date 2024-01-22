using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 5f;
    public int bulletDamage = 10;
    private Rigidbody2D rb;

    public float knockbackForce = 10;

    void Start()
    {
        // Call the DestroySelf() method after destroyDelay seconds
        Invoke("DestroySelf", destroyDelay);
        rb = GetComponent<Rigidbody2D>();
    }

    void DestroySelf()
    {
        // Destroy the object this script is attached to
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 direction = rb.velocity.normalized;
            Vector2 knockback = direction * knockbackForce;
            collision.attachedRigidbody.AddForce(knockback, ForceMode2D.Impulse);
            collision.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        } 
        else if (collision.gameObject.CompareTag("Player"))
        {
        } 
        else
        {
            Destroy(gameObject);
        }
    }


}
