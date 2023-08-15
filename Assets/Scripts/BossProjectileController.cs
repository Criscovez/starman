using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
    public int damageAmount;

    public GameObject impactEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag(KnownTags.Player))
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
