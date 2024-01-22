using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRanged : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float speed = 5;


    private float distance;
    public float closestdistance = 5f;

    // ranged attack
    public float attackCooldown = 2f;
    private float currentAttackCooldown;
    public float bulletSpeed = 15f;
    private bool canAttack = true;
    public float dashAttackDelay = 0.5f;

    //prefabs
    [SerializeField] private Rigidbody2D pfEnemyBullet;

    //text popups
    [SerializeField] public GameObject floatingPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }

        if (distance > closestdistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * 0.4f * Time.deltaTime);

            if (canAttack)
            {
                canAttack = false;
                currentAttackCooldown = attackCooldown;
                popupText("!!!");
                Invoke("rangedAttack", dashAttackDelay);
            }
        }
    }

    void rangedAttack()
    {
        Vector2 direction = player.transform.position - transform.position;

        Rigidbody2D bulletInstance = Instantiate(pfEnemyBullet, transform.position, Quaternion.identity) as Rigidbody2D;

        bulletInstance.velocity = direction.normalized * bulletSpeed;
    }

    public void popupText(string message)
    {
        GameObject points = Instantiate(floatingPoints, transform.position, transform.rotation);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
    }
}
