using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float destroyDelay = 1f;
    public int rockDamage = 20;
    private Rigidbody2D rb;

    public float knockbackForce = 50;

    public int maxHitCount = 3;

    private Dictionary<GameObject, int> hitEnemiesRock = new Dictionary<GameObject, int>();

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
            if (hitEnemiesRock.ContainsKey(collision.gameObject))
            {
                hitEnemiesRock[collision.gameObject] += 1;
            }
            else
            {
                hitEnemiesRock.Add(collision.gameObject, 1);
            }
            Vector2 direction = rb.velocity.normalized;
            Vector2 knockback = direction * knockbackForce;
            collision.attachedRigidbody.AddForce(knockback, ForceMode2D.Impulse);
            if (hitEnemiesRock[collision.gameObject] < maxHitCount)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(rockDamage);
            }
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
