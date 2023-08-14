using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickController : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
    //   Debug.Log("kick");
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
            Debug.Log("kick");
        }
    }
}
