using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float destroyDelay = 1f;
    private Rigidbody2D rb;

    public int maxHitCount = 1;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hitEnemiesRock.ContainsKey(collision.gameObject))
            {
                hitEnemiesRock[collision.gameObject] += 1;
            }
            else
            {
                hitEnemiesRock.Add(collision.gameObject, 1);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
