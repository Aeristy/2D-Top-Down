using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Attack : MonoBehaviour
{
    
    public Collider2D swordCol;
    public float dmg = 1;
    public float knockbackForce = 100;
    public Vector3 faceRight = new Vector3(0.11f, 0.007f, 0);
    public Vector3 faceLeft = new Vector3(-0.35f, 0.007f, 0);
    private void Start()
    {
        
        swordCol = GetComponent<Collider2D>();
    }
    
    public void AttackRight() 
    {
        swordCol.enabled = true;
        transform.localPosition = faceRight;

    }
    public void AttackLeft() 
    {
        swordCol.enabled = true;
        transform.localPosition = faceLeft;
    }
    public void StopAttack()
    {
        swordCol.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (collider.tag == "Enemy" && damageableObject != null)
        {
            Vector3 parentPos = transform.parent.position;
            Vector2 direction = ( collider.transform.position - parentPos).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            damageableObject.TakeDamage(dmg, knockback);
            
        }
       
    }



}
