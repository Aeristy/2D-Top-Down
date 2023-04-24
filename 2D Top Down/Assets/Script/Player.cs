using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float dmg = 1;
    public float _health =3;
    bool _iframe = false;
    public float iframeTime = 1;
    private float iframeTimeElapsed = 0;
    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Invincible)
        {
            iframeTimeElapsed += Time.deltaTime;
            if(iframeTimeElapsed > iframeTime) Invincible = false;
        }
    }
    public float Health
    {
        set
        {
            _health = value;
            if (_health <= 0) gameObject.SendMessage("Deafeated");

        }
        get { return _health; }
    }
    public bool Invincible 
    {   get {
            return _iframe;
        }
        set
        {
            _iframe = value;
            if(_iframe == true)
            {
                iframeTimeElapsed = 0;
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!Invincible)
        {
            Health -= dmg;
            animator.SetTrigger("takeDamage");
            Invincible = true;
        }
    }
    public void TakeDamage(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;

            animator.SetTrigger("takeDamage");
            rb.AddForce(knockback, ForceMode2D.Force);
            Invincible = true;
        }
    }

}
