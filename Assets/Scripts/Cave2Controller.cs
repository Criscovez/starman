using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave2Controller : MonoBehaviour
{
    public GameObject endScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            PlayerInventaryController player = other.GetComponentInParent<PlayerInventaryController>();
            Debug.Log("enter cave");
            if (player.keys>=1) {
                UIController.instance.StartFadeToBlack();
                endScreen.SetActive(true);
            }
            
        }

    }
}
