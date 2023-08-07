using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class PersinstentRemoteData : MonoBehaviour
{


    public bool enableShop = true;

    public int extraCredits = 0;

    public int isActiveNewGamePlus = 0;

    public static PersinstentRemoteData Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



}
