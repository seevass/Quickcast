using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 20;

    //health popup
    [SerializeField] public GameObject floatingPoints;
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = damage.ToString();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
