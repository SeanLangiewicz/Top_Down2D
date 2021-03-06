﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;
   

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;


    //Floating Text Timer
    [Header("Floating Text Timer")]
    public float messageTime = 5f;

    //All Fighters can receive damage and Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)

        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString()+" Damge", 45, Color.red, transform.position, Vector3.zero, messageTime);

            if (hitPoint <=0)
            {
                hitPoint = 0;
                Death();
            }
        }

     }

    protected virtual void Death()
    {
       
    }

}
