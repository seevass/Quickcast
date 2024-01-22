using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100;
    public bool isDead = false;

    //health popup
    [SerializeField] public GameObject floatingPointsPlayer;


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        GameObject points = Instantiate(floatingPointsPlayer, transform.position, Quaternion.identity);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = damage.ToString();


        if (currentHealth <= 0 && isDead == false)
        {
            Debug.Log("Dead: " + currentHealth);
            isDead = true;
        }
    }
}
