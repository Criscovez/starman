using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount;

    public GameObject pickEffect;

    public int cost;

    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(KnownTags.Player))
        {

            if (cost <= PlayerHealthController.instance.currentCredits)
            {
                AudioController.instance.PlaySFXAdjusted(6);
                PlayerHealthController.instance.HealPlayer(healthAmount);
                PlayerHealthController.instance.currentCredits -= cost;
                UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);
                UIController.instance.updateCreditItem(); 

                if (pickEffect != null)
                {
                    Instantiate(pickEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
            else
            {
                unlockText.text = "Creditos insuficientes!";
                unlockText.gameObject.SetActive(true);
            }
        }

    }
}
