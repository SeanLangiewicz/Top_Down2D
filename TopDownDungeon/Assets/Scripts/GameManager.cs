using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("TEMP")]
    public bool resetStats = false;
    public static GameManager instance;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        if (resetStats == true)
        {

        }


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);

    }





    //Resource management
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    //logic
    public int gold;
    public int experence;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }


    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon maxed ?
        if (weaponPrices.Count <= weapon.weaponlevel)
        {
            return false;
        }
        if (gold >= weaponPrices[weapon.weaponlevel])
        {
            gold -= weaponPrices[weapon.weaponlevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experence >= add)
        {
            add += xpTable[r];
            r++;

            // Max Level
            if (r == xpTable.Count)
            {
                return r;
            }
        }

        return r;
    }

    public int GetXPToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experence += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUP();
    }

    public void OnLevelUP()
    {
        Debug.Log("level up");
        player.OnLevelUP();
    }

    //Save State
    /*
        int prefered skin
        int gold
        int experience
        int weapon level


    */
    public void SaveState()
    {

        string s = "";

        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experence.ToString() + "|";
        s += weapon.weaponlevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // Change Player Skin
        gold = int.Parse(data[1]);

        //Experience
        experence = int.Parse(data[2]);
        if (GetCurrentLevel() !=0)
        {
            player.SetLevel(GetCurrentLevel());
        }
        

        // Change weapon level
        weapon.SetWeaponLevel( int.Parse(data[3]));
       

        Debug.Log("Load State");

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}

   
