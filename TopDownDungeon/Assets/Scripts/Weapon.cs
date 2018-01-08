using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable

{

    // Damage Struct
    public int []damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f };

    // Upgrade Section
    public int weaponlevel = 0;
    private SpriteRenderer sprintRenderer;

    //Swing
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        sprintRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
             
            if(Time.time - lastSwing > coolDown)
            {
              lastSwing = Time.time;
            Swing();
            }
                    }
    }
    protected override void OnCollide(Collider2D coll)
    {
       
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")                
                return;

            //Crate new damage object, then send it to the fighter we hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponlevel],
                origin = transform.position,
                pushForce = pushForce[weaponlevel]
            };
                
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
        Debug.Log("Swing");
        
    }

    public void UpgradeWeapon()
    {
        weaponlevel++;
        sprintRenderer.sprite = GameManager.instance.weaponSprites[weaponlevel];

        //Change stats
    }

    public void SetWeaponLevel ( int level)
    {
        weaponlevel = level;
        sprintRenderer.sprite = GameManager.instance.weaponSprites[weaponlevel];
    }
}
