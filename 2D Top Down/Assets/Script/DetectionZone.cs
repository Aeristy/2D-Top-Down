using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> dejectedObjs = new List<Collider2D>();
    public Collider2D col;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            dejectedObjs.Add(col);
        }
    }

}
