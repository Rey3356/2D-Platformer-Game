﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private CircleCollider2D pointA;
    [SerializeField] private CircleCollider2D pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform PointToGoTo;
    [SerializeField] private float speed;
    bool playerDied;
    bool switchUP;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PointToGoTo = pointB.transform;
        anim.SetBool("Walking", true);
        playerDied = false;
        switchUP = false;

    }


    // Update is called once per frame
    void Update()
    {

            if(PointToGoTo == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (switchUP && PointToGoTo == pointB.transform)
            {
                flip();
                switchUP = false;
                PointToGoTo = pointA.transform;
                Debug.Log("Point to go to swithced to A");

            }

            if (switchUP && PointToGoTo == pointA.transform)
            {

                flip();
                switchUP = false;
                PointToGoTo = pointB.transform;
                Debug.Log("Point to go to swithced to B");
            }
        
    }

    private void flip()
    {
        Vector2 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 contact = col.contacts[0].point;
        //Debug.Log("Enenmy collision : " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            PlayerController Cont = col.gameObject.GetComponent<PlayerController>();
            Cont.HurtPlayer(contact);
                
        }

        Debug.Log("Triggered turn");
        if (col.gameObject.tag == "pointA" || col.gameObject.tag == "pointB")
        {
            switchUP = true;

        }

    }


}
