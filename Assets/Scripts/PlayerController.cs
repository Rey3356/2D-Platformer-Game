﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    [SerializeField] private Animator animator;
    [SerializeField] private float Speed = 0f;
    [SerializeField] private float jump = 0f;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject GOScreen;
    [SerializeField] public LevelFinish levelFinish;

    public ParticleSystem BombBlast;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        GOScreen.SetActive(false);
        animator.SetBool("Died", false);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {

        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");

        if(animator.GetBool("Crouch") == false)
        {
            PlayerMove(horiz, vert);
            PlayerMoveAnim(horiz, vert);
        }
        

        //for crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }
        if(isGrounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    private void PlayerMove(float horiz, float vert)
    {
        Vector3 position = transform.position;

        position.x = position.x + (horiz * Speed * Time.deltaTime);
        transform.position = position;

        //for jump
        if(vert>0 && isGrounded == true)
        {
            SoundManager.Instance.PlaySFX(Sounds.playerJump);
            rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);

        }
    }
    public void KeyPickUp()
    {
        Debug.Log("Key Picked Up!");
        scoreManager.setScore(10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("gorund");
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("gorund no");
            isGrounded = false;
        }
    }

    private void PlayerMoveAnim(float horiz, float vert)
    {
        animator.SetFloat("Speed", Mathf.Abs(horiz));
        Vector3 scale = transform.localScale;


        if (horiz < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horiz > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        //for Jump
        if (vert > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        transform.localScale = scale;
    }

    public void HurtPlayer(Vector3 Contact)
    {
        if(playerHealth.health >= 1 )
        {
            SoundManager.Instance.PlaySFX(Sounds.playerHurt);
            playerHealth.LooseHealth();
        }
        else
        {
            if(levelFinish.playerDeath == false)
            {
                BombBlast.gameObject.transform.localPosition = Contact;
                BombBlast.Play();
                SoundManager.Instance.PlaySFX(Sounds.playerDeath);
                animator.SetBool("Died", true);
                StartCoroutine(DeathReload());
            }
            
        }
        
    }

    IEnumerator DeathReload()
    {
        levelFinish.playerDeath = true;
        yield return new WaitForSeconds(3);
        GOScreen.SetActive(true);
    }
}