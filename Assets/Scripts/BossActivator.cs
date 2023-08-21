using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script controla cuando se activa el jefe final, se activa cuando el jugador entra en el trigger
/// </summary>
public class BossActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(KnownTags.Player))
        {
            bossToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
