using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthWhitePickup : MonoBehaviour
{
    public int healthAmount;

    public GameObject pickEffect;

    public int cost;

    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            if (cost <= PlayerHealthController.instance.currentCredits)
            {
                AudioController.instance.PlaySFXAdjusted(5);
                PlayerInventaryController.instance.IncrementHearts(healthAmount);
                //PlayerHealthController.instance.HealPlayer(healthAmount);
                PlayerHealthController.instance.currentCredits -= cost;
                //UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);
                UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);

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
