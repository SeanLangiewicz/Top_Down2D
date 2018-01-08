﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 10;

    protected override void OnCollect()
    {
        if (!isCollected)
        {
            isCollected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += goldAmount;
            GameManager.instance.ShowText("+" + goldAmount + "Gold", 35, Color.yellow, transform.position, Vector3.up * 25, 3f);

        }
    }

}
