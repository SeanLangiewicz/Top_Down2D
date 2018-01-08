using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[Header("TEMP")]
    //public bool resetStats = false;
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

       // if (resetStats == true)
        //{
          //  PlayerPrefs.DeleteAll();
        //}


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
    //public Weapon weapon..

    public FloatingTextManager floatingTextManager;

    //logic
    public int gold;
    public int experence;

    public void ShowText(string msg,int fontSize,Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
        s += "0";

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
        experence = int.Parse(data[2]);
        // Change weapon level

        Debug.Log("Load State");
    }
}

   
