using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    //Text Fields
    public Text levelText, hitPointText, goldText, upgradeCostText, xpText;


    //Logic 
    public int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection

    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //If we went too far away 
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)

                currentCharacterSelection = 0;
            OnSelectionChange();


        }
        else
        {
            currentCharacterSelection--;

            //If we went too far away 
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            currentCharacterSelection = 0;
            OnSelectionChange();
        }
    }
        

    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
        
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update the Character information
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponlevel];
        if (GameManager.instance.weapon.weaponlevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponlevel].ToString();
        }
        

        //meta
        hitPointText.text = GameManager.instance.player.hitPoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();


        //XP Bar
        
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            //Display total xp at max
            xpText.text = GameManager.instance.experence.ToString() + "total experience points";
            xpBar.localScale = Vector3.one;
        }

        else
        {
            int preLevelXP = GameManager.instance.GetXPToLevel(currLevel - 1);
            int currLevelXP = GameManager.instance.GetXPToLevel(currLevel);
           
            int diff = currLevelXP - preLevelXP;
            int currXPIntoLevel = GameManager.instance.experence - preLevelXP;

           
            float completionRatio = currXPIntoLevel / diff;
            
            xpBar.localScale = new Vector3(completionRatio, 2, 2);
            xpText.text = currXPIntoLevel.ToString() + "/" + diff;
        }


     
    }
}
