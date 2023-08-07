using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class InvincItemController : MonoBehaviour
{
    public int invincAmount;

    public GameObject pickEffect;

    public int cost;

    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if (cost <= PlayerHealthController.instance.currentCredits)
            {
                AnalyticsService.Instance.CustomData("productSold", new Dictionary<string, object>()
                {
                    { "id_product", "00000002" },
                    { "product_type", "PowerUp" },
                    { "productAmount", 1 },
                    { "productValue", cost },

                });

                AudioController.instance.PlaySFXAdjusted(7);
                //PlayerHealthController.instance.HealPlayer(healthAmount);
                //PlayerHealthController.instance.invincPlayer(invincAmount);
                PlayerInventaryController.instance.IncrementInvincItems(invincAmount);
                PlayerHealthController.instance.currentCredits -= cost;
                UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);

                if (pickEffect != null)
                {
                    Instantiate(pickEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
            else
            {
                unlockText.text = "insufficient funds!";
                unlockText.gameObject.SetActive(true);
            }
        }

    }
}
