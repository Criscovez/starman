// BossBattle.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private new CameraController camera;
    public Transform camPosition;
    public float camSpeed;

    public int etapa_1, etapa_2;

    public float active, fadeOut, inactive;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetP;
    public float moveSeed;

    public Animator anim;

    public Transform boss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    public GameObject winObjects;

    private bool battleEnded;

    public string bossRef;


    void Start()
    {
        camera = FindObjectOfType<CameraController>();
        camera.enabled = false;

        activeCounter = active;

        shotCounter = timeBetweenShots1;

        AudioController.instance.PlayBossMusic();
    }

    void Update()
    {
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        if (!battleEnded)
        {
            if (BossHealthController.instance.currentHealth > etapa_1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        AudioController.instance.PlaySFXAdjusted(8);
                        fadeCounter = fadeOut;
                        anim.SetTrigger("vanish");
                    }

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;
                        AudioController.instance.PlaySFXAdjusted(8);
                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        
                    }

                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        boss.gameObject.SetActive(false);
                        inactiveCounter = inactive;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        boss.gameObject.SetActive(true);

                        activeCounter = active;
                        shotCounter = timeBetweenShots1;
                    }
                }
            }
            else
            {
                if (targetP == null)
                {
                    targetP = boss;
                    fadeCounter = fadeOut;
                    anim.SetTrigger("vanish");
                    AudioController.instance.PlaySFXAdjusted(8);
                }
                else
                {
                    if (Vector3.Distance(boss.position, targetP.position) > .02f)
                    {
                        boss.position = Vector3.MoveTowards(boss.position, targetP.position, moveSeed * Time.deltaTime);


                        if (Vector3.Distance(boss.position, targetP.position) <= .02f)
                        {
                            fadeCounter = fadeOut;
                            anim.SetTrigger("vanish");
                        }

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (PlayerHealthController.instance.currentHealth > etapa_2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            boss.gameObject.SetActive(false);
                            inactiveCounter = inactive;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetP = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            int whileaOut = 0;
                            while (targetP.position == boss.position && whileaOut < 100)
                            {
                                targetP = spawnPoints[Random.Range(0, spawnPoints.Length)];
                                whileaOut++;
                            }
                            boss.gameObject.SetActive(true);

                            if (PlayerHealthController.instance.currentHealth > etapa_2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }

                        }
                    }
                }
            }
        } else
        {
            fadeCounter -= Time.deltaTime;
            if (fadeCounter < 0)
            {
                if (winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);

                    Debug.Log("winObjects");
                }

                camera.enabled = true;
                Debug.Log("Gano!");

                gameObject.SetActive(false);

                AudioController.instance.PlayLevelMusic();

            }
        }

    }

    public void EndBattle()
    {
        battleEnded = true;
        fadeCounter = fadeOut;
        anim.SetTrigger("vanish");
        boss.GetComponent<Collider2D>().enabled = false;

        BossShoot[] shots = FindObjectsOfType<BossShoot>();
        if(shots.Length > 0)
        {
            foreach(BossShoot bs in shots)
            {
                Destroy(bs.gameObject);
            }
        }


    }
}
