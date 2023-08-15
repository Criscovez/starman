using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
