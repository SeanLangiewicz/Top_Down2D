using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // experience
    public int XPValue = 1;

    // logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool isChasing;
    private bool isCollidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];


    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {

        //Is the player in rage ?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {

                isChasing = true;
            } 
            if (isChasing)
            {
                if (!isCollidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            isChasing = false;
        }

        // Check for overlaps
        isCollidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                if (hits[i] == null)
                    continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                isCollidingWithPlayer = true;
            }

            // The array is not cleaned up, so we do it ourself
            hits[i] = null;



        }


    }
    protected override void Death()
    {
       
        Destroy(gameObject);
        GameManager.instance.experence += XPValue;
        GameManager.instance.ShowText("+" + XPValue + "xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }


}
