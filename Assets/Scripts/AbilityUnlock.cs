using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump;

    public bool unlockGun;

    public TMP_Text unlockText;

    public string unlockMessage;

    public int cost;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Triger Player");
        //Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            if (cost <= PlayerHealthController.instance.currentCredits)
            {
                AnalyticsService.Instance.CustomData("productSold", new Dictionary<string, object>()
                {
                    { "id_product", "00000001" },
                    { "product_type", "PowerUp" },
                    { "productAmount", 1 },
                    { "productValue", cost },

                });

                //Debug.Log("Triger Player");
                PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

                PlayerHealthController.instance.currentCredits -= cost;
                UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);

                if (unlockDoubleJump)
                {
                    player.canDoubleJump = true;
                }

                if (unlockGun)
                {
                    player.canUseGun = true;
                }

                unlockText.transform.parent.SetParent(null);
                unlockText.transform.parent.position = transform.position;

                unlockText.text = unlockMessage;
                unlockText.gameObject.SetActive(true);

                Destroy(unlockText.transform.parent.gameObject, 5f);
                Destroy(gameObject);
            }
            else
            {
                //unlockText.transform.parent.SetParent(null);
                //unlockText.transform.parent.position = transform.position;

                unlockText.text = "insufficient funds!";
                unlockText.gameObject.SetActive(true);

                //Destroy(unlockText.transform.parent.gameObject, 5f);
            }


        }
    }
}
