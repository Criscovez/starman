// UIController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;

    public TMP_Text creditsText;
    public TMP_Text bulletsText;

    public TMP_Text whiteHearts;
    public int invincAmount;
    public TMP_Text invincItems;
    public TMP_Text keys;
    public TMP_Text creditsItems;
    public TMP_Text bulletPackItems;

    [SerializeField] Image blackScreen;



    public float fadeSpeed = 2f;

    private bool fadeIn;

    private bool fadeOut;
    public string mainMenuScene;

    public GameObject pauseScreen;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        PlayerHealthController.instance.UpdateUI();
        creditsItems.text = "X " + PlayerHealthController.instance.currentCredits.ToString();
        bulletPackItems.text = "X " + PlayerInventaryController.instance.bulletPacks.ToString();
        bulletsText.text = PlayerHealthController.instance.currentBullets.ToString();
    }


    void Update()
    {
        if (fadeIn)
        {
            // Debug.Log(blackScreen);
            blackScreen.color = new Color(blackScreen.color.r,
                                          blackScreen.color.g,
                                          blackScreen.color.b,
                                          Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeIn = false;
            }

        }
        else if (fadeOut)
        {
            blackScreen.color = new Color(blackScreen.color.r,
                              blackScreen.color.g,
                              blackScreen.color.b,
                              Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeOut = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pause");
            PauseUnpause();
        }

    }
    public void FillHealth()
    {

        if (PlayerInventaryController.instance.whiteHearts > 0)
        {
            AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "Level1" },
            { "Id__button", "fill_health" },

        });

            AnalyticsService.Instance.CustomData("usedProduct", new Dictionary<string, object>()
                {
                    { "id_product", "00000004" },
                    { "product_type", "energia" },
                    { "idPantalla", "00000001" },
                    { "IDLevel", "00000001" },

                });

            PlayerHealthController.instance.FillHealth();
            PlayerInventaryController.instance.DecrementHeart();
        }
    }

    public void ActiveInvinc()
    {
        if (PlayerInventaryController.instance.invincibleItems > 0)
        {
            AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "Level1" },
            { "Id__button", "active_invinc" },

        });

            AnalyticsService.Instance.CustomData("usedProduct", new Dictionary<string, object>()
                {
                    { "id_product", "00000002" },
                    { "product_type", "PowerUp" },
                    { "idPantalla", "00000001" },
                    { "IDLevel", "00000001" },

                });

            //PlayerHealthController.instance.FillHealth();
            PlayerHealthController.instance.invincPlayer(invincAmount);
            PlayerInventaryController.instance.DecrementInvincItem();
        }
    }

    public void UseBulletPack()
    {
        if (PlayerInventaryController.instance.bulletPacks > 0)
        {
            AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
            {
                { "Id_scene", "Level1" },
                { "Id__button", "use_bullet_pack" },
            });

            AnalyticsService.Instance.CustomData("usedProduct", new Dictionary<string, object>()
            {
                { "id_product", "00000005" },
                { "product_type", "bullets" },
                { "idPantalla", "00000001" },
                { "IDLevel", "00000001" },
            });

            //PlayerHealthController.instance.FillHealth();
            PlayerHealthController.instance.currentBullets = PlayerHealthController.instance.maxBullets;
            UIController.instance.UpdateBulletsUI();
            //PlayerHealthController.instance.currentBullets += invincPlayer(invincAmount);
            PlayerInventaryController.instance.DecrementBulletPack();
        }
    }
    public void UpdateHearts(int hearts)
    {
        whiteHearts.text = "X " + hearts.ToString();
    }

    public void UpdateKeys(int key)
    {
        keys.text = "X " + key.ToString();
    }

    public void UpdateInvincItems(int item)
    {
        invincItems.text = "X " + item.ToString();
    }

    public void UpdateBulletPackItems(int item)
    {
        bulletPackItems.text = "X " + item.ToString();
    }

    public void UpdateBulletPackItem()
    {
        bulletPackItems.text = "X " + PlayerInventaryController.instance.bulletPacks.ToString();
    }

    public void UpdateCreditItems(int item)
    {
        creditsItems.text = "X " + item.ToString();
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }

    public void UpdateBulletsUI()
    {
        bulletsText.text = PlayerHealthController.instance.currentBullets.ToString();
    }



    public void UpdateCredits(int currentCredits)
    {

        //PlayerHealthController player = gameObject.GetComponentInParent<PlayerHealthController>();
        //Debug.Log(player);

        PlayerHealthController.instance.currentCredits = PlayerHealthController.instance.currentCredits + currentCredits;
        //player.currentCredits = System.Int32.Parse(creditsText.text) + currentCredits;

        creditsText.text = PlayerHealthController.instance.currentCredits.ToString();
        creditsItems.text = "X " + PlayerHealthController.instance.currentCredits.ToString();

    }

    public void updateCreditItem()
    {
        creditsItems.text = "X " + PlayerHealthController.instance.currentCredits.ToString();
    }

    public void UpdateCredit(int currentCredits)
    {

        creditsText.text = currentCredits.ToString();
        //creditsItems.text = "X " + PlayerHealthController.instance.currentCredits.ToString();
        //UpdateCreditItems(currentCredits); 

    }

    public void StartFadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
        Debug.Log("fadetoblack");
    }

    public void StartFadeFromBlack()
    {
        fadeIn = false;
        fadeOut = true;
        Debug.Log("fadefromblack");
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);

    }

    public void GoToMainMenuNewGamePlus()
    {
        PlayerPrefs.SetInt("NewGamePlus", 1);
        PersinstentRemoteData.Instance.isActiveNewGamePlus = 1;
        PlayerPrefs.SetInt("Credits", PlayerHealthController.instance.currentCredits);

        Debug.Log("Player pref, Credits: " + PlayerHealthController.instance.currentCredits);


        Time.timeScale = 1;

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);

    }
}
