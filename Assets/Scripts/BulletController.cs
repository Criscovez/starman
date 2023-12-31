// BulletController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D theRB;

    public Vector2 moveDir;

    public GameObject impactEffect;

    public int damageAmount = 1;
    

    void Update()
    {
        if (theRB != null)
        {
            theRB.velocity = moveDir * bulletSpeed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
            
        }

        if(other.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
