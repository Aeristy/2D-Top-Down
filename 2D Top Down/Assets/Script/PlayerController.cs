using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    public Joystick movementJoystick;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Vector3 lastPos;
    Animator animator;
    bool canMove = true;
    public Sword_Attack swordAttack;
    public bool isAlive;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        bool success = TryMove(movementJoystick);      
        if (movementJoystick.joystickVec.x > 0 && isAlive) { spriteRenderer.flipX = false; }
        else if (movementJoystick.joystickVec.x < 0 && isAlive) { spriteRenderer.flipX = true; }
        if (rb.transform.position == lastPos) { success = false; }
        lastPos = rb.transform.position;
        animator.SetBool("isMoving", success);
    }
    private bool TryMove(Joystick joystick)
    {
        if (movementJoystick.joystickVec.y != 0 && canMove)           
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * speed, movementJoystick.joystickVec.y * speed);
            return true;
        }
        else
        {

            rb.velocity = Vector2.zero;
            return false;
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
            swordAttack.AttackLeft();
        else
            swordAttack.AttackRight();

    }
    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    public void LockMovement()
    {
        canMove = false;

    }
    public void UnlockMovement()
    {
        canMove = true;

    }
    
    public void Deafeated()
    {
        animator.SetTrigger("Defeated");
        LockMovement();
        isAlive = false;
        rb.isKinematic = true;
    }



}
