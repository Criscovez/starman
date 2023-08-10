using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventaryController : MonoBehaviour
{
    public int whiteHearts;
    public int keys;
    public int invincibleItems;
    public int bulletPacks;

    public static PlayerInventaryController instance;

    private void Awake()
    {
        

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

    public void IncrementKeys(int key)
    {
        Debug.Log(keys);
        keys += key;
        Debug.Log(key);
        Debug.Log(keys);
        UIController.instance.UpdateKeys(keys);
    }
    public void DecrementKeys()
    {
        Debug.Log(keys);
        keys--;
        //Debug.Log(key);
        Debug.Log(keys);
        UIController.instance.UpdateKeys(keys);
    }

    public void IncrementHearts(int hearts)
    {
        Debug.Log(whiteHearts);
        whiteHearts += hearts;
        Debug.Log(hearts);
        Debug.Log(whiteHearts);
        UIController.instance.UpdateHearts(whiteHearts);
    }

    public void DecrementHeart()
    {
        Debug.Log(whiteHearts);
        whiteHearts--;
        
        Debug.Log(whiteHearts);
        UIController.instance.UpdateHearts(whiteHearts);
    }

    public void IncrementInvincItems(int items)
    {
        //Debug.Log(whiteHearts);
        invincibleItems += items;
        //Debug.Log(hearts);
        //Debug.Log(whiteHearts);
        UIController.instance.UpdateInvincItems(invincibleItems);
    }

    public void DecrementInvincItem()
    {
        //Debug.Log(whiteHearts);
        invincibleItems--;

        //Debug.Log(whiteHearts);
        UIController.instance.UpdateInvincItems(invincibleItems);
    }

    public void IncrementBulletPacks(int items)
    {
        //Debug.Log(whiteHearts);
        bulletPacks += items;
        //Debug.Log(hearts);
        //Debug.Log(whiteHearts);
        UIController.instance.UpdateBulletPackItems(invincibleItems);
    }

    public void DecrementBulletPack()
    {
        //Debug.Log(whiteHearts);
        bulletPacks--;

        //Debug.Log(whiteHearts);
        UIController.instance.UpdateBulletPackItems(bulletPacks);
    }

}
