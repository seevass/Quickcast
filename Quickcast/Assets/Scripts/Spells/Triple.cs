using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triple : MonoBehaviour
{
    public float destroyDelay = 3f;
    public int tripleDamage = 20;
    private Rigidbody2D rb;

    public float knockbackForce = 50;

    public int maxHitCount = 3;

    private Dictionary<GameObject, int> hitEnemiesTriple = new Dictionary<GameObject, int>();

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
            if (hitEnemiesTriple.ContainsKey(collision.gameObject))
            {
                hitEnemiesTriple[collision.gameObject] += 1;
            }
            else
            {
                hitEnemiesTriple.Add(collision.gameObject, 1);
            }
            Vector2 direction = rb.velocity.normalized;
            Vector2 knockback = direction * knockbackForce;
            collision.attachedRigidbody.AddForce(knockback, ForceMode2D.Impulse);
            if (hitEnemiesTriple[collision.gameObject] < maxHitCount)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(tripleDamage);
            }
        }
        else if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Spell"))
        {
            Debug.Log("!!!!");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Object that collided with me: " + collision.gameObject.name);
        }
    }
}
