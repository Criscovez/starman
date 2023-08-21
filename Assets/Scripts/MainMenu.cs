using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;


public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject newGamePlusButton;

    public string storeScene;
    void Start()
    {
        UnityServices.InitializeAsync();
        //AnalyticsService.Instance.s
        AudioController.instance.PlayMainMenuMusic();
        //Debug.Log(RemoteConfig.Instance.enableShop);
        //shopButton.SetActive(RemoteConfig.Instance.enableShop);
       int isActiveNewGamePlus = PlayerPrefs.GetInt("NewGamePlus");
        if (isActiveNewGamePlus == 1)
        {
            newGamePlusButton.SetActive(true);
        }
        else
        {
            newGamePlusButton.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(RemoteConfig.Instance.enableShop);
        //shopButton.SetActive(RemoteConfig.Instance.enableShop);
    }

    public void NewGame()
    {

        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "idPantalla", 0000001 },
            { "nombreBoton", "New Game" },

        });

 
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "new_Game" },

        });

        PlayerPrefs.SetInt("NewGamePlus", 0);
        PlayerPrefs.SetInt("Credits", 0);
        newGamePlusButton.SetActive(false);



        PersinstentRemoteData.Instance.extraCredits = 0;
        SceneManager.LoadScene(newGameScene);
        //SceneManager.LoadScene(levelToLoad);
    }

    public void NewGamePlus()
    {

        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "idPantalla", 0000001 },
            { "nombreBoton", "New Game" },

        });


        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "new_Game" },

        });



        PersinstentRemoteData.Instance.extraCredits = 0;
        SceneManager.LoadScene(newGameScene);
        //SceneManager.LoadScene(levelToLoad);
    }
    public void Credits()
    {
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "credits" },

        });
        AudioController.instance.PlayCreditsMusic();
        //PersinstentRemoteData.Instance.extraCredits = 0;
        SceneManager.LoadScene("Credits");
        //SceneManager.LoadScene(levelToLoad);
    }

    public void Store()
    {
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "store" },

        });
        //PlayerHealthController.instance.currentCredits = RemoteConfig.Instance.extraCredits;
        //Debug.Log(PlayerHealthController.instance.currentCredits);
        PersinstentRemoteData.Instance.extraCredits = RemoteConfig.Instance.extraCredits;
        SceneManager.LoadScene(storeScene);
        //SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", "main_scene" },
            { "Id__button", "quit" },

        });
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Debug.Log("Game Quit");
    }
}
