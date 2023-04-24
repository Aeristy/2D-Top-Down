using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float dmg = 1;
    public float _health;
    public float moveSpeed = 0.5f;
    public float knockbackForce = 100;
    Animator animator;
    Rigidbody2D rb;
    public DetectionZone zone;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(zone.dejectedObjs.Count>0)
        {
            Vector2 direction = (zone.dejectedObjs[0].transform.position - transform.position).normalized;
            spriteRenderer.flipX = direction.x < 0;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

        }
    }
    public float Health
    {
        set
        {
            _health = value;
            if (_health <= 0) Deafeated();

        }
        get { return _health; }
    }

    public void Deafeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        animator.SetTrigger("takeDamage");
        Debug.Log("Hit " + dmg);
    }
    public void TakeDamage(float damage, Vector2 knockback)
    {
        Health -= damage;
        animator.SetTrigger("takeDamage");
        Debug.Log("Hit " + damage);
        rb.AddForce(knockback,ForceMode2D.Impulse);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (collider.tag == "Player" && damageableObject != null)
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            damageableObject.TakeDamage(dmg, knockback);
           
        }
    }
    

    
}
