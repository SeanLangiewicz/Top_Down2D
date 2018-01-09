using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;




    }





    //Resource management
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public RectTransform hitPointBar;
    public GameObject hud;
    public GameObject menu;

    public FloatingTextManager floatingTextManager;
    public Animator deathMenuAnim;

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
        OnHitPointChange();
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

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // Change Player Skin
        gold = int.Parse(data[1]);

        //Experience
        experence = int.Parse(data[2]);
        if (GetCurrentLevel() != 0)
        {
            player.SetLevel(GetCurrentLevel());
        }


        // Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));


    }

    //Hit Point Bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Death Menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
}

   
