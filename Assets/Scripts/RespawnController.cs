// RespawnController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    private void Awake()
    {
        instance = this;
    }

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;

    public GameObject deathEffect; 

    private void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;

        respawnPoint = thePlayer.transform.position;

    }

    void Update()
    {
        
    }

    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        thePlayer.SetActive(false);
        if(deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }


}
