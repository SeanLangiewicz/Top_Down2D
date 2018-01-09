using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover 
{

    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void FixedUpdate()
    {
        // left / right input
        float x = Input.GetAxisRaw("Horizontal");
        // up / down input
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }

       
    }
   public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];

    }
    public void OnLevelUP()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUP();
        }
    }

    public void Heal ( int healingAmount)
    {
        if (hitPoint == maxHitPoint)
        {
            return;
        }
      
            hitPoint += healingAmount;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "Health", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitPointChange();


    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
        {
            return;
        }
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }
    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
