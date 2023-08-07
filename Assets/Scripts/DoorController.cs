// DoorController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    private PlayerController thePlayer;

    public bool PlayerExiting { get; private set; }

    public Transform exitPoint;
    public float movePlayerSpeed;

    public string levelToLoad;

    private string pathScene = "Assets/Scenes/";

    private void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (PlayerExiting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!PlayerExiting)
            {
                thePlayer.canMove = false;

                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo()
    {

        PlayerExiting = true;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);

        thePlayer.canMove = true;

        UIController.instance.StartFadeFromBlack();

        //SceneManager.LoadScene(pathScene + levelToLoad);
        Debug.Log(levelToLoad);
        SceneManager.LoadScene(levelToLoad);

    }

}
