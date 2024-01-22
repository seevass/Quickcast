using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float speed = 4;


    private float distance;
    public float closestdistance = 4f;

    // melee dash attack
    public float attackCooldown = 5f;
    private float currentAttackCooldown;
    public float dashSpeed = 0.5f;
    private bool canAttack = true;
    private Vector2 dashVector;
    public float positionDelay = 0.3f;
    public float dashAttackDelay = 0.5f;

    //text popups
    [SerializeField] public GameObject floatingPoints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        } else
        {
            canAttack = true;
        }

        if (distance > closestdistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * 0.4f * Time.deltaTime);

            if (canAttack)
            {
                canAttack = false;
                currentAttackCooldown = attackCooldown;
                popupText("!!!");
                Invoke("dashAttack", dashAttackDelay);
            }
        }
    }

    void dashAttack()
    {
        Vector2 direction = player.transform.position - transform.position;
        dashVector = direction * dashSpeed;
        Invoke("dash", 0.5f);
    }

    void dash()
    {
        rb.AddForce(dashVector * 1.5f, ForceMode2D.Impulse);
    }

    public void popupText(string message)
    {
        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
    }
}
