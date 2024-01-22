using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;
    [SerializeField] EnemyHealth health;
    private float totalHealth;
    public float healthbarScalar = 1.4f;

    void Start()
    {
        localScale = transform.localScale;
        totalHealth = health.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (health.currentHealth / totalHealth) * healthbarScalar;
        transform.localScale = localScale;
    }
}
