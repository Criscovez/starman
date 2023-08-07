using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{


    public void MainMenu()
    {
        
        //PersinstentRemoteData.Instance.extraCredits = 0;
        SceneManager.LoadScene("Main Menu");
        //SceneManager.LoadScene(levelToLoad);
    }
}
