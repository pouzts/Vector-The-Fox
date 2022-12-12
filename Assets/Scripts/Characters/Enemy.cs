using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyable
{
    [SerializeField] int points;
    [SerializeField] float speed;
    [SerializeField] Health health;
    [SerializeField] Damage damage;
    [SerializeField] GameObject playerCheck;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Collider2D wallCheck;

    float playerRadius = 0.2f;
    bool isHurted = false;
    
    bool flip;

    void Start()
    {
        flip = true;
    }

    void Update()
    {
        if (!flip)
        {
            rb2d.velocity = new Vector2(speed * Time.deltaTime, rb2d.velocity.y);
            renderer.flipX = true;
        }
        else 
        {
            rb2d.velocity = new Vector2(-speed * Time.deltaTime, rb2d.velocity.y);
            renderer.flipX = false;
        }

        if (isHurted)
        {
            health.Damage(1);
            if (health.health <= 0)
            {
                GameManager.Instance.Score += points;
            }
            isHurted = false;
        }
    }

    void FixedUpdate()
    {
        isHurted = Physics2D.OverlapCircle(playerCheck.transform.position, playerRadius).gameObject.CompareTag("Player");

        flip = !Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) || wallCheck.IsTouchingLayers(groundLayer);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
