using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyController : MonoBehaviour
{
    public int KeyAmount;

    public GameObject pickEffect;

    public int cost;

    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(KnownTags.Player))
        {

            if (cost <= PlayerHealthController.instance.currentCredits)
            {
                PlayerInventaryController.instance.IncrementKeys(KeyAmount);
                //PlayerHealthController.instance.HealPlayer(healthAmount);
                PlayerHealthController.instance.currentCredits -= cost;
                //UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);

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
