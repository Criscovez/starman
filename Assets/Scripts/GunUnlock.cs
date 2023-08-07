using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class GunUnlock : MonoBehaviour
{
    public bool unlockDoubleJump;

    public bool unlockGun;

    public TMP_Text unlockText;

    public string unlockMessage;

    public int cost;
    public int bulletsAmount;
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
                    { "id_product", "00000003" },
                    { "product_type", "PowerUp" },
                    { "productAmount", 1 },
                    { "productValue", cost },

                });

                AudioController.instance.PlaySFXAdjusted(9);
                PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

                //Debug.Log("Triger Player");


                PlayerHealthController.instance.currentCredits -= cost;
                //PlayerHealthController.instance.currentBullets += bulletsAmount;

                if (PlayerHealthController.instance.currentBullets == 0)
                {
                    PlayerHealthController.instance.currentBullets += bulletsAmount;
                    UIController.instance.UpdateBulletsUI();
                } 
                else if (PlayerHealthController.instance.currentBullets == PlayerHealthController.instance.maxBullets)
                {
                    //UIController.instance.UpdateCredit(PlayerHealthController.instance.maxBullets);
                    PlayerInventaryController.instance.IncrementBulletPacks(1);
                }
               // else { }

                UIController.instance.UpdateCredit(PlayerHealthController.instance.currentCredits);
                UIController.instance.UpdateBulletsUI();
                //UIController.instance.bulletsText.text = PlayerHealthController.instance.currentBullets.ToString();

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
