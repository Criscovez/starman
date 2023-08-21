// PlayerHealthController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    //[HideInInspector]
    public int currentHealth;
    [SerializeField]
    int maxHealth;

    public float invicibilityLength;

    private float invincCounter;

    public float flashLength;

    private float flashCounter;

    public SpriteRenderer[] playerSprite;

    private int _currentCredits;

    public int currentCredits
    {
        get { return _currentCredits; }
        set
        {
            int newValue = _currentCredits - value;
            if (newValue < 0)
            {
                newValue = 0;
            }
        }

    }

    public void UpdateCredits(int deltaValue)
    {
        int newValue = _currentCredits + deltaValue;
        if (newValue < 0)
        {
            newValue = 0;
        }

        _currentCredits = newValue;
    }

    public int maxCredits;

    public int currentBullets;
    public int maxBullets;


    private void Awake()
    {
        //instance = this;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
        
       string  m_Scene = SceneManager.GetActiveScene().name;
       // Debug.Log(m_Scene);

        if(m_Scene == "StoreScene")
        {
            currentCredits = PersinstentRemoteData.Instance.extraCredits;
        }

        if(PersinstentRemoteData.Instance.isActiveNewGamePlus == 1|| PlayerPrefs.GetInt("NewGamePlus") == 1)
        {
            currentCredits += PlayerPrefs.GetInt("Credits");
        }

        UIController.instance.UpdateCredit(currentCredits);
        //sceneName = m_Scene.name;
        //UIController.instance.bulletsText.text = PlayerHealthController.instance.currentBullets.ToString();

        //currentCredits = PlayerPrefs.GetInt("extraCredits");
    }


    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if(flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprite)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if(invincCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprite)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }

        }
    }
   
    public void UpdateUI()
    {
        UIController.instance.UpdateCredit(currentCredits);
        UIController.instance.UpdateHealth(currentHealth,maxHealth);
        UIController.instance.UpdateHearts(PlayerInventaryController.instance.whiteHearts);
        UIController.instance.UpdateInvincItems(PlayerInventaryController.instance.invincibleItems);
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invincCounter <= 0)
        {

            int damageAmountBefore = currentHealth;
                currentHealth -= damageAmount;

                if (currentHealth <= 0)
                {

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "IDLevel", "00000001" },
                    { "mapPosition", transform.position.ToString() },
                    { "damageType", "Enemy_01" },
                    { "IdDamageType", "00000001" },
                    { "damageAmountBefore", damageAmountBefore },
                    { "damageAmount", damageAmount },


                };

                AnalyticsService.Instance.CustomData("enemyDamageLocation", parameters);

                currentHealth = 0;

                //gameObject.SetActive(false);

                RespawnController.instance.Respawn();
                }
                else
                {
                    invincCounter = invicibilityLength;
                }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);

        }
    }

    public void invincPlayer(int invincAmount)
    {
        if (invincCounter <= 0)
        {

           invincCounter = invincAmount;

        }
    }

    public void FillHealth()
    {

        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
        Debug.Log(currentHealth);
        Debug.Log(maxHealth);

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);

    }

}
