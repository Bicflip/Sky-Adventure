using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //variabile globale pt start
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private AudioSource footstep;

    //sorter machine


    private enum State {idle, running, jumping, falling, hurt};
    private State state = State.idle;
    


    //variabile simplificate
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private float hurtforce = 10f;
    public int cherries = 0;
    public int health = 3;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        footstep = GetComponent<AudioSource>();
        PermanentUI.perm.HPAmount.text = PermanentUI.perm.health.ToString();
    }

    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            
            Destroy(collision.gameObject);
            PermanentUI.perm.cherries += 1;
            PermanentUI.perm.cherrytext.text = PermanentUI.perm.cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy= other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                jump();
            }
            else
            {
                state = State.hurt;
                handleHP(); 
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //inamicul e in dreapta, deci ar trebui sa primesc daune si sa ma misc in stanga
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                }
                else
                {
                    //inamicul e in stanga, deci ar trebui sa primesc daune si sa ma misc in dreapta
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);

                }
            }
        }
    }

    private void handleHP()
    {
        PermanentUI.perm.health -= 1;
        PermanentUI.perm.HPAmount.text = PermanentUI.perm.health.ToString();
        if (PermanentUI.perm.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PermanentUI.perm.Reset();
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Miscarile Modelului(s-d)
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);            
        }
        else
        {
            if (hDirection > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);                
            }
        }
        //sarit
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jump();
        }
    }

    private void jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            state = State.jumping;
    }

    private void AnimationState()
    { 

        if(state == State.jumping)
        {
            if(rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            { 
                state = State.idle; 
            }
        }
        else if(state==State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state=State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 1f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
    
    private void Footstep()
    {
        footstep.Play();
    }
}