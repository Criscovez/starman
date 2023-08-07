// BossHealthController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{

    public static BossHealthController instance;

    public Slider bossHeathSlider;
     public int   currentHealth = 30;

    public BossBattle boss;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bossHeathSlider.maxValue = currentHealth;
        bossHeathSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth<=0)
        {
            currentHealth = 0;
            boss.EndBattle();
        }

        bossHeathSlider.value = currentHealth;
    }


}
