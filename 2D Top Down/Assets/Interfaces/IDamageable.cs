using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public float Health { get; set; }
    void TakeDamage(float damage, Vector2 knockback);

    void TakeDamage(float damage);
    
}
