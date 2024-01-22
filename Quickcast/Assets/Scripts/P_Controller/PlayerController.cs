using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform CameraTarget;
    public GameObject GestureScreen;

    //movement
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;
    public Vector2 movementInput;
    public Vector2 previousMovementInput;
    private bool usingPrevInput;

    //dashing
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    public float dashCounter;
    private float dashCoolCounter;

    public bool isDashing = false;

    //knockback
    public float knockSpeed;
    public float knockLength = .5f, knockbackCooldown = 1f;
    public float knockCounter;
    private float knockCoolCounter;
    public bool isKnock = false;

    //player damage values
    public int meleeDamage = 5;
    public int rangedDamage = 10;

    //input for pause
    public bool isPaused = false;

    //health
    public PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        movementInput.Normalize();
        rb.velocity = movementInput * activeMoveSpeed;

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                if (usingPrevInput)
                {
                    movementInput = Vector2.zero;
                    usingPrevInput = false;
                }
                isDashing = false;
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (knockCoolCounter > 0)
        {
            knockCoolCounter -= Time.deltaTime;
        }

        if (knockCounter > 0)
        {
            knockCounter -= Time.deltaTime;
            if (knockCounter <= 0)
            {
                movementInput = Vector2.zero;

                isKnock = false;
                activeMoveSpeed = moveSpeed;
                knockCoolCounter = dashCooldown;
            }
        }

        if (knockCoolCounter > 0)
        {
            knockCoolCounter -= Time.deltaTime;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        if (isDashing == false && isKnock == false)
        {
            previousMovementInput = movementInput;
            movementInput = inputValue.Get<Vector2>();
        }
    }

    private void OnDash()
    {
        if (dashCoolCounter <=0 && dashCounter <= 0)
        {
            isDashing = true;
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;
            if (movementInput == Vector2.zero)
            {
                movementInput = previousMovementInput;
                usingPrevInput = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (knockCoolCounter <= 0 && knockCounter <= 0)
            {
                isKnock = true;
                activeMoveSpeed = knockSpeed;
                knockCounter = knockLength;
                movementInput = (transform.position - collision.transform.position).normalized;
            }
            if (isKnock!)
            {
            GetComponent<PlayerHealth>().TakeDamage(meleeDamage);
            }
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (knockCoolCounter <= 0 && knockCounter <= 0)
            {
                isKnock = true;
                activeMoveSpeed = knockSpeed;
                knockCounter = knockLength;
                movementInput = (transform.position - collision.transform.position).normalized;
            }
            if (isKnock!)
            {
                GetComponent<PlayerHealth>().TakeDamage(rangedDamage);
            }
        }
    }

    private void OnPauseGame()
    {
        isPaused = !isPaused;
    }
}
