using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectile : MonoBehaviour
{
    //wand coordinates
    [SerializeField] private Transform wandEnd;
    [SerializeField] private Transform wandPosition;

    //spell speed
    [SerializeField] PlayerMagicCasting magicCasting;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float rockSpeed = 10f;
    [SerializeField] private float tripleSpeed = 10f;
    [SerializeField] private float tripleRockSpeed = 20f;

    //prefabs
    [SerializeField] private Rigidbody2D pfBullet;
    [SerializeField] private Rigidbody2D pfRock;
    [SerializeField] private Rigidbody2D pfTriple;

    //combo prefabs
    [SerializeField] private Rigidbody2D pfTripleRock;

    //combo checking
    public bool hasCombo = false;

    //shooting speed
    [SerializeField] public float shootCooldown = 0.5f;
    public float currentShotCooldown;
    public bool canShoot = false;

    //spellshot cooldowns/interval
    public float basicCooldown = 0.5f;
    public float rockCooldown = 1f;
    public float tripleCooldown = 1f;
    public float tripleInterval = .3f;

    private void Update()
    {
        if (currentShotCooldown > 0)
        {
            currentShotCooldown -= Time.deltaTime;
        } else
        {
            canShoot = true;
        }

        if (hasCombo == false)
        {
            if (magicCasting.spells.Count >= 2)
            {
                if (magicCasting.spells[0] == "triangle" && magicCasting.spells[1] == "circle")
                {
                    magicCasting.popupText_Side("Triple Rock Ready!");
                    hasCombo = true;
                }
            }
        }
    }

    private void OnFire()
    {   
        if (canShoot)
        {
            canShoot = false;

            if (magicCasting.spells.Count > 0)
            {
                //check for combos (largest to smallest)
                if (hasCombo)
                {
                    if (magicCasting.spells[0] == "triangle" && magicCasting.spells[1] == "circle")
                    {
                        shootTriple("cancelInvokeTriRock", "tripleHelperTriRock");
                        hasCombo = false;
                        magicCasting.spells.RemoveAt(0);
                        magicCasting.spells.RemoveAt(0);
                    }
                } else
                {
                    switch (magicCasting.spells[0])
                    {
                        case "circle":
                            spell1();
                            break;
                        case "triangle":
                            spell2();
                            break;
                    }
                }
                //check singular 
            } else
            {
                shootBasicAttack();
            }

        }
    }

    private void shootRaycast()
    {
        Vector3 shootDirection;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wandEnd.position;
        BulletRaycast.Shoot(wandEnd.position, shootDirection);
    }
    private void shootBasicAttack()
    {
        Vector3 shootDirection;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wandEnd.position;

        //shootDirection = shootDirection.normalized;

        Rigidbody2D bulletInstance = Instantiate(pfBullet, wandEnd.position, wandEnd.rotation) as Rigidbody2D;

        bulletInstance.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * bulletSpeed;
        currentShotCooldown = basicCooldown;
    }

    private void shootRock()
    {
        Vector3 shootDirection;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wandEnd.position;

        //shootDirection = shootDirection.normalized;

        Rigidbody2D bulletInstance = Instantiate(pfRock, wandEnd.position, wandEnd.rotation) as Rigidbody2D;

        bulletInstance.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * rockSpeed;
        currentShotCooldown = rockCooldown;
    }

    private void shootTriple(string cancelInvoke, string tripleHelper)
    {
        currentShotCooldown = tripleCooldown;
        Invoke(cancelInvoke, 0.9f);
        InvokeRepeating(tripleHelper, 0, tripleInterval);
    }

    private void cancelInvoke()
    {
        CancelInvoke("tripleHelper");
    }

    private void tripleHelper()
    {
        Vector3 shootDirection;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wandEnd.position;

        //shootDirection = shootDirection.normalized;

        Rigidbody2D bulletInstance = Instantiate(pfTriple, wandEnd.position, wandEnd.rotation) as Rigidbody2D;

        bulletInstance.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * tripleSpeed;
    }

    private void cancelInvokeTriRock()
    {
        CancelInvoke("tripleHelperTriRock");
    }

    private void tripleHelperTriRock()
    {
        Vector3 shootDirection;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wandEnd.position;

        //shootDirection = shootDirection.normalized;

        Rigidbody2D bulletInstance = Instantiate(pfTripleRock, wandEnd.position, wandEnd.rotation) as Rigidbody2D;

        bulletInstance.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * tripleRockSpeed;
    }

    private void spell1()
    {
        if (magicCasting.spells[0] == "circle")
        {
            shootRock();
            magicCasting.spells.RemoveAt(0);
        }
    }

    private void spell2()
    {
        if (magicCasting.spells[0] == "triangle")
        {
            shootTriple("cancelInvoke","tripleHelper");
            magicCasting.spells.RemoveAt(0);
        }
    }
}
